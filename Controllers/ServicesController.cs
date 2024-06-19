using ASWebEssentials.Data;
using ASWebEssentials.Models;
using ASWebEssentials.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;

namespace ASWebEssentials.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext Context;
        public ServicesController(ApplicationDbContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            List<Services> Allservices = Context.Services.ToList();

            return View(Allservices);
        }

        public IActionResult Details(int id)
        {
            Services? services = Context.Services.FirstOrDefault(x => x.Id == id);

            if (services != null)
            {
                var relatedService = Context.Service.FirstOrDefault(x => x.Id == services.ServiceId);

                if (relatedService != null)
                {
                    services.Service = relatedService;
                }
                else
                {
                    return NotFound();
                }

                return View(services);
            }

            return NotFound();
        }
    }
}
