using ASWebEssentials.Data;
using ASWebEssentials.Models;
using ASWebEssentials.Models.ContactModel;
using ASWebEssentials.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ASWebEssentials.Controllers
{
    public class ContactController(ApplicationDbContext context, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext Context = context;
        private readonly UserManager<AppUser> _userManager = userManager;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(Ticket model)
        {
            if (ModelState.IsValid)
            {
                string? userName = User?.Identity?.Name;
                if (userName != null)
                {
                    var user = await _userManager.FindByNameAsync(userName);
                    if (user != null)
                    {
                        var ticket = new Ticket
                        {
                            FullName = model.FullName,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            About = model.About,
                            Quest = model.Quest,
                            UserName = user.UserName,
                            Status = "open",
                        };

                        Context.Ticket.Add(ticket);
                        Context.SaveChanges();
                        return RedirectToAction("MyTickets");
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> MyTickets()
        {
            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);

                Console.WriteLine("user", user!.UserName);
                if (user != null)
                {
                    List<Ticket> userTickets = await Context.Ticket
                                                .Include(t => t.TicketRes)
                                                .Where(t => t.UserName == user.UserName)
                                                .ToListAsync();

                    return View(userTickets);
                }
            }

            // Handle the case when the user is not authenticated or current user is null
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyMessages()
        {
            string? userName = User?.Identity!.Name;
            AppUser? user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);

                if (user != null)
                {
					List<AdminMess> userMessages = await Context.AdminMess
									.Include(t => t.MessageRes)
									.Where(t => t.Recipient == user.UserName || t.Status == "general")
									.ToListAsync();

					return View(userMessages);
                }
            }

            // Handle the case when the user is not authenticated or current user is null
            return RedirectToAction("Index");
        }

        public IActionResult UpdateSelectedTicket(int? itemId)
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

            return PartialView("_MessageResp", ticket);
        }

    
        public async Task<IActionResult> SaveResponse(int id, string reply)
        {
            string? userName = User?.Identity?.Name;
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var ticket = await Context.Ticket.FirstOrDefaultAsync(t => t.Id == id);

                    if (ticket == null)
                    {
                        return NotFound();
                    }

                    var response = new TicketRes
                    {
                        TicketId = ticket.Id,
                        RespText = reply,
                        ResDate = DateTime.Now,
                        UserName = user.UserName
                    };

                    // Save the response to the database
                    Context.TicketRes.Add(response);
                    Context.SaveChanges();
                }
            }
            var updatedTicket = await Context.Ticket
                                     .Include(t => t.TicketRes)
                                     .FirstOrDefaultAsync(t => t.Id == id);

            if (updatedTicket == null)
            {
                return NotFound();
            }

            return PartialView("_TicketDetails", updatedTicket);

        }

        public IActionResult SelectedMessageDetails(int? MId)
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

        public async Task<IActionResult> MessResponseSaving(int id, string reply)
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

    }
}
