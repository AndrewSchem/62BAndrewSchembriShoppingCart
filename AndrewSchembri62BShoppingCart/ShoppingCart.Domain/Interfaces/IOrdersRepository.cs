using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
	public interface IOrdersRepository
	{
		IQueryable<Order> GetOrders();
		Order GetOrder(int id);
		void AddOrder(Order order);
		void DeleteOrder(Order order);
	}
}
