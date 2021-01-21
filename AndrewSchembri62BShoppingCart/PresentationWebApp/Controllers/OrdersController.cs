using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;

namespace PresentationWebApp.Controllers
{
	public class OrdersController : Controller
	{
		private readonly ICartsService _cartsService;
		private readonly IOrdersService _ordersService;
		private readonly IOrderDetailsService _orderDetailsService;
		private IWebHostEnvironment _env;

		public OrdersController(IOrdersService ordersService, IOrderDetailsService orderDetailsService, ICartsService cartsService, IWebHostEnvironment env)
		{
			_ordersService = ordersService;
			_orderDetailsService = orderDetailsService;
			_cartsService = cartsService;
			_env = env;
		}

		[Authorize(Roles = "User, Admin")]
		public IActionResult Index(string email)
		{
			var list = _orderDetailsService.GetOrderDetails(email);
			return View(list);
		}
	}
}
