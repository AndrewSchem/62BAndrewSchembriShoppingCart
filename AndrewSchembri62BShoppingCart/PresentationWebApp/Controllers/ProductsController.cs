using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using X.PagedList;

namespace PresentationWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICartsService _cartsService;
        private readonly ICategoriesService _categoriesService;
        private IWebHostEnvironment _env;
        public ProductsController(IProductsService productsService, ICategoriesService categoriesService,
             ICartsService cartsService ,IWebHostEnvironment env )
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _cartsService = cartsService;
            _env = env;
        }

        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var list = _productsService.GetProducts();
            int pageSize = 10;
            var onePageOfProducts = list.ToPagedList(pageNumber, pageSize);

            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;

            return View(onePageOfProducts);
        }

        [HttpPost]
        public IActionResult Search(string keyword, int? page) //using a form, and the select list must have name attribute = category
        {
            var pageNumber = page ?? 1;
            var list = _productsService.GetProducts(keyword).ToList();

            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;

            return View("Index", list.ToPagedList(pageNumber, 10));
        }

        [HttpPost]
        public IActionResult SearchCategory(int category, int? page) //using a form, and the select list must have name attribute = category
        {
            var pageNumber = page ?? 1;
            var list = _productsService.GetProducts(category).ToList();

            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;

            return View("Index", list.ToPagedList(pageNumber, 10));
        }


        public IActionResult Details(Guid id)
        {
            var p = _productsService.GetProduct(id);
            return View(p);
        }

        //the engine will load a page with empty fields
        [HttpGet]
        [Authorize (Roles ="Admin")] //is going to be accessed only by authenticated users
        public IActionResult Create()
        {
            //fetch a list of categories
            var listOfCategeories = _categoriesService.GetCategories();

            //we pass the categories to the page
            ViewBag.Categories = listOfCategeories;

            return View();
        }

        //here details input by the user will be received
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile f)
        {
            try
            {
                if(f !=  null)
                {
                    if(f.FileName.Length > 0)
                    {
                        if (data.Stock > 0) {
                            //C:\Users\Ryan\source\repos\SWD62BEP\SWD62BEP\Solution3\PresentationWebApp\wwwroot
                            string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                            string newFilenameWithAbsolutePath = _env.WebRootPath + @"\Images\" + newFilename;

                            using (var stream = System.IO.File.Create(newFilenameWithAbsolutePath))
                            {
                                f.CopyTo(stream);
                            }

                            data.ImageUrl = @"\Images\" + newFilename;

                            _productsService.AddProduct(data);

                            TempData["feedback"] = "Product was added successfully";
						}
						else{
                            TempData["warning"] = "Product Stock Cannot Be Less Than 1!";
                        }
					}
					else
					{
                        TempData["warning"] = "Product add an Image!";
                    }
				}
				else
				{
                    TempData["warning"] = "Product add an Image!";
                }


            }
            catch (Exception ex)
            {
                //log error
                TempData["warning"] = "Product was not added!";
            }

           var listOfCategeories = _categoriesService.GetCategories();
           ViewBag.Categories = listOfCategeories;
            return View(data);
        
        } //fiddler, burp, zap, postman

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productsService.DeleteProduct(id);
                TempData["feedback"] = "Product was deleted";
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not deleted";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Hide(Guid id)
		{
            try
            {
                _productsService.HideProduct(id);
                TempData["feedback"] = "Product is Now Hidden!";
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product is Not Hidden!";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Show(Guid id)
        {
            try
            {
                _productsService.ShowProduct(id);
                TempData["feedback"] = "Product is Now Shown!";
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product is Not Shown!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult AddToCart(Guid id, int quantity)
        //public IActionResult AddToCart(Guid id, string email)
        {
            try
            {
                if (id != null && quantity > 0) {
                        _cartsService.AddToCart(_productsService.GetProduct(id), quantity, User.Identity.Name);
				}else
				{
                    TempData["warning"] = "Product was not added! (ID or Quantity Error)";

                }

                TempData["feedback"] = "Product was added successfully";
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not added! ";
            }

            return RedirectToAction("Index");
        }
    }
}
