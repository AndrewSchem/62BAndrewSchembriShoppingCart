using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
	public interface IOrderDetailsService
	{
		IQueryable<OrderDetailsViewModel> GetOrderDetails(string email);
		OrderDetailsViewModel GetOrderDetail(int id);
		void AddOrderDetail(int orderId, Guid productId, int quantity, double price);
		void DeleteOrderDetail(OrderDetailsViewModel order);
	}
}
