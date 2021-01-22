using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ProductsController> _logger;
        private IWebHostEnvironment _env;
        public ProductsController(IProductsService productsService, ICategoriesService categoriesService,
             ICartsService cartsService, ILogger<ProductsController> logger, IWebHostEnvironment env )
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _cartsService = cartsService;
            _logger = logger;
            _env = env;
        }

        //https://www.youtube.com/watch?v=vnxN_zBisIo
        public IActionResult Index(int? page) //Page Number
        {
            var pageNumber = page ?? 1; //If Page Number is Not Specified Page Number = 1
            var list = _productsService.GetProducts(); //Get All Products
            int pageSize = 10; //Specify Products Per Page
            var onePageOfProducts = list.ToPagedList(pageNumber, pageSize); //Give X.PagedList Parameters

            var listOfCategeories = _categoriesService.GetCategories(); //Categories For Filter
            ViewBag.Categories = listOfCategeories; //Set All Categories

            return View(onePageOfProducts); //Send List of Products
        }

        [HttpPost]
        public IActionResult Search(string keyword, int? page) //If Keyword Inserted
        {
            var pageNumber = page ?? 1;
            var list = _productsService.GetProducts(keyword).ToList(); //Get Products With Keyword

            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;

            return View("Index", list.ToPagedList(pageNumber, 10));
        }

        [HttpPost]
        public IActionResult SearchCategory(int category, int? page) //If Category Id inserted 
        {
            var pageNumber = page ?? 1;
            var list = _productsService.GetProducts(category).ToList(); //Get Products With Category

            var listOfCategeories = _categoriesService.GetCategories();
            ViewBag.Categories = listOfCategeories;

            return View("Index", list.ToPagedList(pageNumber, 10));
        }


        public IActionResult Details(Guid id)
        {
            var p = _productsService.GetProduct(id); //Get Details of Item
            return View(p);
        }

        [HttpGet]
        [Authorize (Roles ="Admin")]
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
                _logger.LogError("Product Was Not Added! " + ex);
                TempData["warning"] = "Product was not added!";
            }

           var listOfCategeories = _categoriesService.GetCategories();
           ViewBag.Categories = listOfCategeories;
           return View(data);
        
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productsService.DeleteProduct(id); //Delete Product With Id
                TempData["feedback"] = "Product was deleted";
            }
            catch (Exception ex)
            {
                _logger.LogError("Product Was Not Deleted! " + ex);
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
                _productsService.HideProduct(id); //Hide Product With ID
                TempData["feedback"] = "Product is Now Hidden!";
            }
            catch (Exception ex)
            {
                _logger.LogError("Product Was Not Hidden! " + ex);
                TempData["warning"] = "Product is Not Hidden!";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Show(Guid id)
        {
            try
            {
                _productsService.ShowProduct(id); //Show Product with ID
                TempData["feedback"] = "Product is Now Shown!";
            }
            catch (Exception ex)
            {
                _logger.LogError("Product Was Not Shown! " + ex);
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
                        _cartsService.AddToCart(_productsService.GetProduct(id), quantity, User.Identity.Name); //Add To Cart ProductID, with Quantity and Email
				}else
				{
                    TempData["warning"] = "Product was not added! (ID or Quantity Error)";

                }

                TempData["feedback"] = "Product was added successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError("Product Was Not Added! " + ex);
                TempData["warning"] = "Product was not added! ";
            }

            return RedirectToAction("Index");
        }
    }
}
