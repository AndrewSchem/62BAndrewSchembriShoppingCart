using ShoppingCart.Application.Services;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
	public interface IOrdersService
	{
		IQueryable<OrderViewModel> GetOrders(string email);
		OrderViewModel GetOrder(int id);
		int AddOrder(OrderViewModel order);
		void DeleteOrder(OrderViewModel order);
	}
}
