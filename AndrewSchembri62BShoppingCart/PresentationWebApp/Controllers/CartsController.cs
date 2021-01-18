using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Index()
        {
            //......
            return View();
        }
    }
}
