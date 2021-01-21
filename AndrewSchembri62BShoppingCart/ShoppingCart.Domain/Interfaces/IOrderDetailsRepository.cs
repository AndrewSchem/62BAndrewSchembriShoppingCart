using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
	public interface IOrderDetailsRepository
	{
		IQueryable<OrderDetails> GetOrderDetails();
		OrderDetails GetOrderDetail(int id);
		void AddOrderDetail(OrderDetails order);
		void DeleteOrderDetail(OrderDetails order);
	}
}
