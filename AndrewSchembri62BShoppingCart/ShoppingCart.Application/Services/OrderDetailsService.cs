using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
	public class OrderDetailsService : IOrderDetailsService
	{
		private IMapper _mapper;
		private IOrderDetailsRepository _orderDetailsRepo;
		public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IMapper mapper)
		{
			_mapper = mapper;
			_orderDetailsRepo = orderDetailsRepository;
		}

		public void AddOrderDetail(int orderId, Guid productId, int quantity, double price)
		{
			var myOrder = new OrderDetails();
			myOrder.OrderId = orderId;
			myOrder.ProductId = productId;
			myOrder.Quantity = quantity;
			myOrder.Price = price;
			_orderDetailsRepo.AddOrderDetail(myOrder); //Add Order Detail using Var of OrderDetails
		}

		public void DeleteOrderDetail(OrderDetailsViewModel order)
		{
			var myOrder = _mapper.Map<OrderDetails>(order);
			_orderDetailsRepo.DeleteOrderDetail(myOrder); //Delete Order by using OrderDetails
		}

		public OrderDetailsViewModel GetOrderDetail(int id)
		{
			var myOrder = _orderDetailsRepo.GetOrderDetail(id);
			var result = _mapper.Map<OrderDetailsViewModel>(myOrder);
			return result; //Return OrderDetails where id = id
		}

		public IQueryable<OrderDetailsViewModel> GetOrderDetails(string email)
		{
			var orders = _orderDetailsRepo.GetOrderDetails().Where(x => x.Order.UserEmail == email).ProjectTo<OrderDetailsViewModel>(_mapper.ConfigurationProvider);
			return orders; //Get OrderDetails Where OrderDetails.Order.UserEmail = email
		}
	}
}
