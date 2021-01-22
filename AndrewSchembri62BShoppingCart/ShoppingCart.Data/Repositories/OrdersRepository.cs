using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		ShoppingCartDbContext _context;
		public OrdersRepository(ShoppingCartDbContext context)
		{
			_context = context;
		}

		public void AddOrder(Order order)
		{
			_context.Orders.Add(order); //Add Order
			_context.SaveChanges();
		}

		public void DeleteOrder(Order order)
		{
			_context.Orders.Remove(order); //Remove Order
			_context.SaveChanges();
		}

		public IQueryable<Order> GetOrders()
		{
			return _context.Orders; //Get All Orders
		}

		public Order GetOrder(int id)
		{
			return _context.Orders.SingleOrDefault(x => x.Id == id); //Get Order With Id
		}
	}
}
