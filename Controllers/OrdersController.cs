using ASWebEssentials.Data;
using ASWebEssentials.Models;
using ASWebEssentials.Models.CartModels;
using ASWebEssentials.Models.OrderModels;
using ASWebEssentials.Models.ProductModels;
using ASWebEssentials.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASWebEssentials.Controllers
{
    [Authorize]
    public class OrdersController(UserManager<AppUser> userManager, ApplicationDbContext context) : Controller
    { 
        private readonly ApplicationDbContext Context = context;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<IActionResult> IndexAsync()
        {
            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            Orders? UserOrdersHistory = Context.Orders.FirstOrDefault(x => x.User == user);

            if (UserOrdersHistory != null)
            {
                List<OrderItem> UserOrderItems = Context.Entry(UserOrdersHistory!)
                                                      .Collection(b => b.OrderItem!)
                                                      .Query()
                                                      .ToList();
                if (UserOrderItems != null && UserOrderItems.Count > 0)
                {
                    foreach (var item in UserOrderItems)
                    {
                        item.Cart = Context.Cart.FirstOrDefault(x => x.Id == item.CartId);
                        if (item.Cart != null)
                        {
                            List<CartItem> OrderCartItems = Context.Entry(item.Cart!)
                                                      .Collection(b => b.Items!)
                                                      .Query()
                                                      .ToList();
                            foreach (var cartItem in OrderCartItems)
                            {
                                item.Cart.TotalQuantity += cartItem.Quantity;
                                cartItem.Product = Context.Products
                                                .Include(p => p.Options)
                                                .FirstOrDefault(p => p.Id == cartItem.ProdId);
                                
                            }
                        }
                    }
                }
                return View(UserOrdersHistory);

            } else
            {
                Orders? UserOrders= new Orders();
                UserOrders.User = user;
                UserOrders.OrderItem = new List<OrderItem>();
                return View(UserOrders);
            }

                
        }

		public async Task<IActionResult> DetailsAsync(string OrderAS)
		{
			if (string.IsNullOrEmpty(OrderAS))
			{
				return BadRequest("OrderAS is null or empty.");
			}

            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            OrderItem? GetOrderItem = Context.OrderItems.FirstOrDefault(x => x.OrderItemId == OrderAS);

			if (GetOrderItem == null)
			{
				return NotFound($"OrderItem with OrderItemId '{OrderAS}' not found.");
			}

            GetOrderItem.Cart = Context.Cart.FirstOrDefault(x => x.Id == GetOrderItem.CartId);
            if (GetOrderItem.Cart != null)
            {
                List<CartItem> OrderCartItems = Context.Entry(GetOrderItem.Cart!)
                                          .Collection(b => b.Items!)
                                          .Query()
                                          .ToList();
                foreach (var cartItem in OrderCartItems)
                {
                    GetOrderItem.Cart.TotalQuantity += cartItem.Quantity;
                    cartItem.Product = Context.Products
                                    .Include(p => p.Options)
                                    .FirstOrDefault(p => p.Id == cartItem.ProdId);

                }
            }

            OrderPaymentVM orderPaymentVM = new OrderPaymentVM();
            orderPaymentVM.OrderItem = GetOrderItem;
            orderPaymentVM.PaymentCard = Context.PaymentCards.FirstOrDefault(x => x.User == user);

            return View(orderPaymentVM);
		}


		public async Task<IActionResult> ConfirmOrderAsync(string UserName,decimal TotalPrice)
        {
            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            Cart? OrderCart = Context.Cart.FirstOrDefault(x => x.User!.UserName == UserName && x.CartStatus == "open");
            if (OrderCart != null)
            {
                OrderCart.TotalPrice = TotalPrice;
                OrderCart.Items = [.. Context.CartItem.Where(x => x.CartId == OrderCart.Id)];
                
                if (OrderCart.Items.Count > 0)
                {
                    foreach (CartItem item in OrderCart.Items)
                    {
                        Product? product = Context.Products.FirstOrDefault(x => x.Id == item.ProdId);

                        List<Option> ProdOpt = Context.Entry(product!)
                                                      .Collection(b => b.Options!)
                                                      .Query()
                                                      .ToList();

                        Option? ProductOption = ProdOpt.FirstOrDefault(o => o.OptionName == item.Name);
                        item.Product = product;
                    };
                }
            }
            PaymentCard? PaymentCard = Context.PaymentCards.FirstOrDefault(x => x.User!.UserName == UserName);

            OrderItem? NewOrderItem = new();
            NewOrderItem.CartId = OrderCart!.Id;
            NewOrderItem.Cart = new Cart();
            NewOrderItem.Cart = OrderCart;

            string orderNumber = OrderHelper.GenerateOrderNumber(Context);

            NewOrderItem.OrderItemId = orderNumber;

            Orders? UserOrders = Context.Orders.FirstOrDefault(x => x.User!.UserName == UserName);

            if (UserOrders == null)
            {
                UserOrders = new Orders();
                UserOrders.User = user;
                Context.Orders.Add(UserOrders);
                Context.SaveChanges();
                UserOrders = Context.Orders.FirstOrDefault(x => x.User!.UserName == UserName);
            }

            NewOrderItem.OrdersId = UserOrders!.Id;
            NewOrderItem.Status = "processing";


            OrderCart.CartStatus = "close";

            Context.Cart.Update(OrderCart);
            Context.OrderItems.Add(NewOrderItem);
            Context.SaveChanges();

            return View(NewOrderItem);
        }

        public class OrderHelper
        {
            public static string GenerateOrderNumber(ApplicationDbContext context)
            {
                Random random = new();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                string randomString = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                string orderNumber = "AS" + randomString;

                OrderItem? orderItem = context.OrderItems.FirstOrDefault(x => x.OrderItemId == orderNumber);

                if (orderItem != null)
                {
                    while (orderItem != null)
                    {
                        randomString = new string(Enumerable.Repeat(chars, 6)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                        orderNumber = "AS" + randomString;
                        orderItem = context.OrderItems.FirstOrDefault(x => x.OrderItemId == orderNumber);
                    }
                }
                return orderNumber;
            }

        }


    }
}
