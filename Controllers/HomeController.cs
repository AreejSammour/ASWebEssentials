using ASWebEssentials.Data;
using ASWebEssentials.Models;
using ASWebEssentials.Models.ProductModels;
using ASWebEssentials.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASWebEssentials.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext Context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Features = new List<Feature>();
            homeVM.Services = new List<Services>();
            homeVM.Products = new List<Product>();

            List<Feature> AllFeatures = Context.Feature.ToList();
            homeVM.Features = AllFeatures;

            List <Services> AllServices = Context.Services.ToList();
            homeVM.Services = AllServices;

            List<Product> AllProducts = Context.Products
                                        .Include(p => p.Options)
                                        .Include(p => p.Categories)
                                        .ToList();
            homeVM.Products = AllProducts;

            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
