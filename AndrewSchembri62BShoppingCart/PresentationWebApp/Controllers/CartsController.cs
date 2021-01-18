using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationWebApp.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartsService _cartsService;
        private readonly IProductsService _productsService;
        private IWebHostEnvironment _env;

        public CartsController(IProductsService productsService, ICategoriesService categoriesService,ICartsService cartsService, IWebHostEnvironment env)
        {
            _productsService = productsService;
            _cartsService = cartsService;
            _env = env;
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Index()
        {
            var list = _cartsService.GetCarts("andrewschembri1@hotmail.com");
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _cartsService.DeleteFromCart(id);
                TempData["feedback"] = "Product was deleted";
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not deleted"; //Change from ViewData to TempData
            }

            return RedirectToAction("Index");
        }
    }
}
