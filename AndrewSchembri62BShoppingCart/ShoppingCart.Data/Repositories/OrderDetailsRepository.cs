using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
	public class OrderDetailsRepository : IOrderDetailsRepository
	{
		ShoppingCartDbContext _context;
		public OrderDetailsRepository(ShoppingCartDbContext context)
		{
			_context = context;
		}

		public void AddOrderDetail(OrderDetails order)
		{
			_context.OrderDetails.Add(order);
			_context.SaveChanges();
		}

		public void DeleteOrderDetail(OrderDetails order)
		{
			_context.OrderDetails.Remove(order);
			_context.SaveChanges();
		}

		public OrderDetails GetOrderDetail(int id)
		{
			return _context.OrderDetails.SingleOrDefault(x => x.Id == id);
		}

		public IQueryable<OrderDetails> GetOrderDetails()
		{
			return _context.OrderDetails;
		}
	}
}
