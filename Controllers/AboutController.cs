using ASWebEssentials.Data;
using ASWebEssentials.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using ASWebEssentials.Models;

namespace ASWebEssentials.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext Context;
        public AboutController(ApplicationDbContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            List<Feature> AllFeatures = Context.Feature.ToList();
            return View(AllFeatures);
        }
    }
}
