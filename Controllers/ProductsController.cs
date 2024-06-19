using ASWebEssentials.Data;
using Microsoft.AspNetCore.Mvc;
using ASWebEssentials.Models.ProductModels;
using ASWebEssentials.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ASWebEssentials.Models.CartModels;
using ASWebEssentials.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ASWebEssentials.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ApplicationDbContext Context;
		public ProductsController(ApplicationDbContext context) 
		{ 
			Context = context;
		}

		public IActionResult Index()
		{
			List<Product> AllProducts = Context.Products.ToList();

			foreach (Product item in AllProducts)
			{
                var CategoriesList = Context.Entry(item)
							.Collection(b => b.Categories!)
							.Query()
							.ToList();
				item.Categories = new List<Category>();
				item.Categories = CategoriesList;
			}

            List<Category> AllCategories = Context.Categorys.ToList();

			foreach (Category item in AllCategories)
			{
				var ProductsList = Context.Entry(item)
                            .Collection(b => b.Products!)
                            .Query()
                            .ToList();
                foreach (var prod in ProductsList)
                {
                    prod.Options = Context.Entry(prod!)
                                                      .Collection(b => b.Options!)
                                                      .Query()
                                                      .ToList();
                }
				item.Products = new List<Product>();
				item.Products = ProductsList;
            }

			var CatProdVM = new ProductCategoryVM();
			CatProdVM.Categories = new List<Category>();
			CatProdVM.Categories = AllCategories;
			CatProdVM.Products = new List<Product>();
			CatProdVM.Products = AllProducts;

			CatProdVM.SelectedCategory = "ALL";
			CatProdVM.SelectedProducts = new List<Product>();
            CatProdVM.SelectedProducts = AllProducts;


            return View(CatProdVM);
		}

        public IActionResult ShowProduct(int id, string catname)
		{
			var product = Context.Products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                var productOptions = Context.Entry(product!)
                                .Collection(b => b.Options!)
                                .Query()
                                .ToList();

                product!.Options = new List<Option>();
                product.Options = productOptions;
            }

            Category category = new Category();
            List<Product> ProductsList = new List<Product>();
            ProdCatVM viewModel = new ProdCatVM();

            if ( catname != "all")
            {
                category = Context.Categorys.FirstOrDefault(x => x.CatName == catname)!;

                if (category == null)
                {
                    return NotFound(); // Return a 404 Not Found response if the category is not found
                }

                ProductsList = Context.Entry(category)
                                .Collection(b => b.Products!)
                                .Query()
                                .ToList();
                category.Products = new List<Product>();
                category.Products = ProductsList;

                if (category.Products == null)
                {
                    throw new InvalidOperationException("Unable to load products for the category.");
                }
            } else
            {
                ProductsList = Context.Products.ToList();
                category.Products = new List<Product>();
                category.Products = ProductsList;
                category.CatName = catname;
            }

            viewModel = new ProdCatVM
            {
                Product = product,
                Category = category
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult UpdateProductList(int catId, string catName)
        {
            List<Product> productList;

            if (catId == 0)
            {
                productList = Context.Products
                                      .Include(p => p.Categories)
                                      .Include(p => p.Options)
                                      .ToList();
            }
            else
            {
                productList = Context.Products
                                      .Where(p => p.Categories!.Any(c => c.Id == catId))
                                      .Include(p => p.Categories)
                                      .Include(p => p.Options)
                                      .ToList();
            }

            return PartialView("_ProductList", productList);
        }

        [HttpGet]
        public IActionResult UpdateProductList2(int prodId, string catName)
        {
            if (prodId == 0)
            {
                var productList = Context.Products.ToList();
                return PartialView("_ProductList", productList);
            }
            else
            {
                List<Product> productList = new List<Product>();
                Product? product = Context.Products
                                    .Include(p => p.Categories)
                                    .Include(p => p.Options!)
                                    .FirstOrDefault(p => p.Id == prodId);
                if (product != null)
                productList.Add(product);
                return PartialView("_ProductList", productList);
            }
        }
        
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            List<Category> categories = Context.Categorys.ToList();

            ViewBag.Categories = categories;
            return PartialView("_CreatePartial");
        }
		
        [HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(Product product)
		{
			if (!ModelState.IsValid)
			{
                // If the model is not valid, return the view with validation errors
                return PartialView("_CreatePartial", product);
            }
            product.Categories ??= new List<Category>();
            var selectedCategoryIds = Request.Form["Categories"];
            foreach (var categoryId in selectedCategoryIds)
            {
                // Retrieve category from database based on categoryId
                var category = Context.Categorys.Find(int.Parse(categoryId!));

                // Add category to product's categories
                product.Categories.Add(category!);
            }

            Context.Products.Add(product);
			Context.SaveChanges();

			TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToAction("Index", "AdminDash");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete()
        {
           List<Product> ProductsList = Context.Products
                                   .Include(p => p.Options)
                                   .ToList();
            return PartialView("_DeletePartial", ProductsList);
        }

        [Authorize(Roles = "admin")]

        public IActionResult DeleteOption(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = Context.Options.Find(id);

            if (option == null)
            {
                return NotFound();
            }

            var productId = option.ProductId;
            List<Option> options = Context.Options.Where(x => x.ProductId == productId).ToList();

            if (options.Count == 1)
            {
                var product = Context.Products.FirstOrDefault(x => x.Id == productId);
                if (product != null)
                {
                    Context.Products.Remove(product);
                    Context.SaveChanges();
                }
            }
            else
            {
                Context.Options.Remove(option);
                Context.SaveChanges();
            }

            TempData["success"] = "Product option deleted successfully";
            return RedirectToAction("Index", "AdminDash");
        }

        [Authorize(Roles = "admin")]
        public IActionResult UpdateOptions()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateCategory()
        {
            List<Category> categories = Context.Categorys.ToList();

            ViewBag.Categories = categories;
            
            return PartialView("_CreateCategory");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateCategory", category);
            }
            

            Context.Categorys.Add(category);
            Context.SaveChanges();

            TempData["SuccessMessage"] = "Category added successfully!";
            return RedirectToAction("Index", "AdminDash");
        }


    }
}
