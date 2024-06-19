using ASWebEssentials.Data;
using ASWebEssentials.Models;
using ASWebEssentials.Models.CartModels;
using ASWebEssentials.Models.ProductModels;
using ASWebEssentials.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASWebEssentials.Controllers
{
    public class CheckoutsController(ApplicationDbContext context, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext Context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
    
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            CheckoutVM CheckoutVMToShow = new CheckoutVM();
            CheckoutVMToShow.UserName = user?.UserName;
            CheckoutVMToShow.Email = user?.Email;
            CheckoutVMToShow.Address = user?.Address;

            List<CartVM> CartVMList = [];

            Cart? CartOpen = Context.Cart.FirstOrDefault(c => c.CartStatus == "open" && c.User!.Id == user!.Id);

            int ItemsQuantity = 0;
            decimal FinalPrice = 0;

            if (CartOpen != null)
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
                        ItemsQuantity += item.Quantity;
                        FinalPrice += CartVMItem.TotalItemPrice;
                    };
                }
            }
            CheckoutVMToShow.CartVMList = CartVMList;
            CheckoutVMToShow.TotalItems = ItemsQuantity;
            CheckoutVMToShow.TotalPrice = FinalPrice;

            PaymentCard? paymentCard = Context.PaymentCards.FirstOrDefault(c => c.User!.Id == user!.Id);

            if (paymentCard != null)
            {
                CheckoutVMToShow.PaymentCard = paymentCard;
            }

            return View(CheckoutVMToShow);
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

        public async Task<IActionResult> AddPaymentMethod(PaymentCard paymentCard)
        {
            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            paymentCard.User = user;

            Context.PaymentCards.Add(paymentCard);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ChangePaymentMethod(int CardId)
        {
            PaymentCard? paymentCardToRemove = Context.PaymentCards.FirstOrDefault(c => c.Id == CardId);

            if (paymentCardToRemove == null)

            {
                return NotFound();
            }


            Context.PaymentCards.Remove(paymentCardToRemove);
            Context.SaveChanges();
            TempData["success"] = "Item deleted successfully";
            return RedirectToAction("Index");
        }


    }
}
