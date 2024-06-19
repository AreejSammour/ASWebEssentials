using ASWebEssentials.Data;
using ASWebEssentials.Models;
using ASWebEssentials.Models.CartModels;
using ASWebEssentials.Models.ContactModel;
using ASWebEssentials.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;

namespace ASWebEssentials.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminDashController(ApplicationDbContext context, UserManager<AppUser> userManager) : Controller
        {
            private readonly ApplicationDbContext Context = context;
            private readonly UserManager<AppUser> _userManager = userManager;

            public IActionResult Index()
            {
                string? successMessage = TempData["SuccessMessage"] as string;

                // Pass the success message to the view
                ViewData["SuccessMessage"] = successMessage;
                return View();
            }

        public IActionResult GetSelectedTicket(int? itemId)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            Ticket? ticket = Context.Ticket
                                  .Include(t => t.TicketRes)
                                  .FirstOrDefault(t => t.Id == itemId);

            if (ticket == null)
            {
                // Handle case where ticket is not found
                return NotFound();
            }

            return PartialView("~/Views/Shared/_MessageResp.cshtml", ticket);
        
        }

        public IActionResult TicketStatus()
        {
            return PartialView("_TicketStatus");
        }

        public IActionResult SendMessage()
        {
            return PartialView("_SendMessage");
        }

        public IActionResult ReplayMessage()
        {
            return PartialView("_ReplayMessage");
        }

        public IActionResult TicketUpdate()
        {
            return PartialView("_TicketUpdate");
        }

        public IActionResult CloseTicket(int? itemId)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            Ticket? ticket = Context.Ticket
                                  .Include(t => t.TicketRes)
                                  .FirstOrDefault(t => t.Id == itemId);

            if (ticket == null)
            {
                // Handle case where ticket is not found
                return NotFound();
            }

            if (ticket.Status == "closed")
            {
                return Content("This ticket is already closed."); // Return a message indicating the ticket is already closed
            }

            ticket.Status = "close";
            Context.Ticket.Update(ticket);
            Context.SaveChanges();

            return PartialView("~/Views/Shared/_MessageResp.cshtml", ticket);

        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(AdminMess model)
        {
            if (ModelState.IsValid)
            {
                string? userName = User?.Identity?.Name;
                if (userName != null)
                {
                    var user = await _userManager.FindByNameAsync(userName);
                    if (user != null)
                    {
                        var adminMess = new AdminMess
                        {
                            MessAbout = model.MessAbout,
                            MessBody = model.MessBody,
                        };

                        if (model.Recipient != null)
                        {
                            adminMess.Recipient = model.Recipient;
                            adminMess.Status = "private";
                        } else
                        {
                            adminMess.Status = "general";
                        }

                        Context.AdminMess.Add(adminMess);
                        Context.SaveChanges();

                        // Prepare the message to be returned to the client-side JavaScript
                        string message = "Message sent successfully!";
                        if (model.Recipient != null)
                        {
                            message += " Message sent to: " + model.Recipient;
                        }

                        // Return a JSON response with the message
                        return Json(new { success = true, message });
                    }
                }
            }

            // If ModelState is not valid or other conditions fail, return a JSON response with error message
            return Json(new { success = false, message = "Failed to send message. Please try again later." });
        }
   
        public IActionResult GetSelectedMessage(int? MId)
        {
            if (MId == null)
            {
                return BadRequest();
            }

            AdminMess? adminMess = Context.AdminMess
                    .Include(t => t.MessageRes) 
                    .FirstOrDefault(t => t.Id == MId);

            if (adminMess == null)
            {
                // Handle case where ticket is not found
                return NotFound();
            }

            return PartialView("~/Views/Shared/AdminPartial/_AdminMessageResp.cshtml", adminMess);

        }

        public async Task<IActionResult> MessResponse(int id, string reply)
        {
            string? userName = User?.Identity?.Name;
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var message = await Context.AdminMess.FirstOrDefaultAsync(t => t.Id == id);

                    if (message == null)
                    {
                        return NotFound();
                    }

                    var response = new MessageRes
                    {
                        AdminMessId = message.Id,
                        RespText = reply,
                        ResDate = DateTime.Now,
                        UserName = user.UserName
                    };

                    // Save the response to the database
                    Context.MessageRes.Add(response);
                    Context.SaveChanges();
                }
            }

            var updatedmessage = await Context.AdminMess
                                    .Include(t => t.MessageRes)
                                    .FirstOrDefaultAsync(t => t.Id == id);


            if (updatedmessage == null)
            {
                return NotFound();
            }

            return PartialView("AdminPartial/_MessageDetails", updatedmessage);

        }

        public IActionResult ProductsByCategory()
        {
            var data = Context.Categorys
                .Select(c => new
                {
                    CategoryName = c.CatName,
                    Count = c.Products!.Count
                }).ToList();


            ViewBag.ChartData = System.Text.Json.JsonSerializer.Serialize(data);
            return PartialView("_ProductsByCategoryPartial");
        }

        public IActionResult TotalSales()
        {
            var rawData = Context.OrderItems
                .GroupBy(o => new { o.PlacedDate.Month, o.PlacedDate.Year })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalSales = g.Sum(o => o.Cart!.TotalPrice) 
                })
                .ToList(); 
            
            var salesData = rawData
                .Select(g => new
                {
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Month),
                    MonthNumber = g.Month,
                    Year = g.Year,
                    TotalSales = g.TotalSales
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.MonthNumber)
                .ToList();

            return PartialView("_TotalSalesPartial", salesData);

        }

    }
}
