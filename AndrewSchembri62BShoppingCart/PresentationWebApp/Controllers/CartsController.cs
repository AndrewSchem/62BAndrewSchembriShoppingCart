using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
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
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrdersService _ordersService;
        private IWebHostEnvironment _env;

        public CartsController(IProductsService productsService, IOrdersService ordersService, IOrderDetailsService orderDetailsService,ICartsService cartsService, IWebHostEnvironment env)
        {
            _orderDetailsService = orderDetailsService;
            _ordersService = ordersService;
            _productsService = productsService;
            _cartsService = cartsService;
            _env = env;
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Index()
        {
            string email = User.Identity.Name;
            var list = _cartsService.GetCarts(email);
            return View(list);
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
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

            return RedirectToAction("Index", new { email = User.Identity.Name });
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult AddOrder()
        {
            try
            {
                string email = User.Identity.Name;
                if (email != null)
                {
                    var CartToAdd = _cartsService.GetCarts(email);
                    if (CartToAdd.Count > 0)
                    {
                        var orderModel = new OrderViewModel();
                        orderModel.DatePlaced = DateTime.Now;
                        orderModel.UserEmail = User.Identity.Name;
                        int orderId = _ordersService.AddOrder(orderModel);

                        foreach (var c in CartToAdd)
                        {
                            if (c.Product.Stock > 0 && c.Product.Stock >= c.Quantity) {
                                double price = (c.Quantity * c.Product.Price);
                                int quantity = c.Quantity;
                                Guid productId = c.Product.Id;
                                
                                _orderDetailsService.AddOrderDetail(orderId, productId, quantity, price);
                                _cartsService.DeleteFromCart(c.Id);

                                _productsService.DecreaseStock(c.Product, c.Quantity);
                            }
                        }

                        TempData["feedback"] = "Order Created";
                    }
                    else
                    {
                        TempData["warning"] = "Order was Not Created: Empty Cart";
                    }
				}
				else
				{
                    TempData["warning"] = "Order was Not Created: Invalid Email";
                }
            }
            catch (Exception ex) {
                TempData["warning"] = "Order was Not Created" + ex;
            }
            return RedirectToAction("Index");
        }
    }
}
