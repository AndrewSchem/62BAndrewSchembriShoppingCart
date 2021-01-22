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
            string email = User.Identity.Name; //User Email
            var list = _cartsService.GetCarts(email); //Get From Cart
            return View(list); //View User Cart
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cartsService.DeleteFromCart(id); //Delete Product From Cart
                TempData["feedback"] = "Product was deleted";
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not deleted";
            }

            return RedirectToAction("Index", new { email = User.Identity.Name }); //Redircet with User Email
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult AddOrder()
        {
            try
            {
                string email = User.Identity.Name;
                if (email != null)
                {
                    var CartToAdd = _cartsService.GetCarts(email); //Get All From Cart
                    if (CartToAdd.Count > 0) //If Cart Not Empty
                    {
                        var orderModel = new OrderViewModel();
                        orderModel.DatePlaced = DateTime.Now; //Setting Order DateTime
                        orderModel.UserEmail = User.Identity.Name; //Setting Order Email
                        int orderId = _ordersService.AddOrder(orderModel); //Adding Order

                        foreach (var c in CartToAdd) //Loop Every Item in Cart
                        {
                            if (c.Product.Stock > 0 && c.Product.Stock >= c.Quantity) { //If Quantity Wanted is Greater than Stock Keep Item in Cart and Dont Create Order
                                double price = (c.Quantity * c.Product.Price); //Setting Price is Product Price for Quantity
                                int quantity = c.Quantity; // Setting Quantity
                                Guid productId = c.Product.Id;  //Set Product Id
                                
                                _orderDetailsService.AddOrderDetail(orderId, productId, quantity, price); //Add Order Detail
                                _cartsService.DeleteFromCart(c.Id); //Delete Product From Cart

                                _productsService.DecreaseStock(c.Product, c.Quantity); //Decrease From Stock
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
