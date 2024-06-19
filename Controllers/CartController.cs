using ASWebEssentials.Data;
using ASWebEssentials.Models;
using ASWebEssentials.Models.CartModels;
using ASWebEssentials.Models.ProductModels;
using ASWebEssentials.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;

namespace ASWebEssentials.Controllers
{
    [Authorize]
    public class CartController(ApplicationDbContext context, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext Context = context;
        private readonly UserManager<AppUser> _userManager = userManager;

        [Route("Cart/index")]
        public async Task<IActionResult> Index()
        {
            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            List<CartVM> CartVMList = [];

            Cart? CartOpen = Context.Cart.FirstOrDefault(c => c.CartStatus == "open" && c.User!.Id == user!.Id);

            if (CartOpen == null)
            {
                CartOpen = new()
                {
                    TotalPrice = 0,
                    Items = [],
                    User = user,
                    CartStatus = "open"
                };

                Context.Cart.Add(CartOpen);
                Context.SaveChanges();
            }
            else
            {
                CartOpen.Items = [.. Context.CartItem.Where(x => x.CartId == CartOpen.Id),];
                var x = 0;

                if (CartOpen.Items.Count > 0)
                {
                    foreach (CartItem item in CartOpen.Items)
                    {
                        Product? product = Context.Products.FirstOrDefault(x => x.Id == item.ProdId);

                        List<Option> ProdOpt = Context.Entry(product!)
                                                      .Collection(b => b.Options!)
                                                      .Query()
                                                      .ToList();

                        Option? ProductOption = ProdOpt.FirstOrDefault(o => o.OptionName == item.Name);

                        CartVM CartVMItem = new()
                        {
                            Id = x,
                            Name = item.Name,
                            ProdName = product!.ProdName,
                            CartItemId = item.Id,
                            Image = ProductOption!.ImageUrl,
                            ItemPrice = ProductOption.Price,
                            Quantity = item.Quantity,
                            TotalItemPrice = ProductOption.Price * item.Quantity,
                        };
                        CartVMList.Add(CartVMItem);
                        x++;
                    };
                }
            }

            return View(CartVMList);
        }

        [Route("Cart/AddToCartAsync")]
        public async Task<IActionResult> AddToCartAsync(string selectedOption, int productId, int quantity)
        {
            Product? product = Context.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Index", "Home"); 
            }

            Option? ProductOption = Context.Entry(product!)
                                        .Collection(b => b.Options!)
                                        .Query()
                                        .FirstOrDefault(o => o.OptionName == selectedOption);

            if (ProductOption != null)
            {
                product.Options = [ProductOption];
            }

            CartItem newCartItem = new CartItem();
            newCartItem.Name = selectedOption;
            newCartItem.ProdId = productId;
            newCartItem.Quantity = quantity;

            decimal optionPrice = 0; // Default value

            if (product.Options != null && product.Options.Any())
            {
                optionPrice = product.Options.First().Price;
            }
            newCartItem.QuantPrice = optionPrice * quantity;

            string? userName = User.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            Cart? openCart = Context.Cart.FirstOrDefault(c => c.CartStatus == "open" && c.User == user);
            if (openCart != null)
            {
                newCartItem.CartId = openCart.Id;
            }
            else
            {
                Cart newCart = new() { TotalPrice = 0, Items = [], User = user, CartStatus = "open" };
                Context.Cart.Add(newCart);
                Context.SaveChanges();

                newCartItem.CartId = newCart.Id;
                openCart = newCart;
            }

            Context.CartItem.Add(newCartItem);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

   
        public IActionResult UpdateQuantity(int itemId, int newQuantity)
        {
            var cartItem = Context.CartItem.FirstOrDefault(item => item.Id == itemId);

            if (cartItem == null)
            {
                return NotFound(); 
            }

            cartItem.QuantPrice = (newQuantity * cartItem.QuantPrice) / cartItem.Quantity;

            cartItem.Quantity = newQuantity;

            Context.CartItem.Update(cartItem);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public IActionResult Delete(int? id)
        {
            var cartItemToDelete = Context.CartItem.FirstOrDefault(item => item.Id == id);

            if (cartItemToDelete == null)

            {
                return NotFound();
            }


            Context.CartItem.Remove(cartItemToDelete);
            Context.SaveChanges();
            TempData["success"] = "Item deleted successfully";

            return RedirectToAction("Index");
        }

    }
}
