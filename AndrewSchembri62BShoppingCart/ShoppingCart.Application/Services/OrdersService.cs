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
	public class OrdersService : IOrdersService
	{
		private IMapper _mapper;
		private IOrdersRepository _ordersRepo;
		public OrdersService(IOrdersRepository ordersRepository, IMapper mapper)
		{
			_mapper = mapper;
			_ordersRepo = ordersRepository;
		}

		public int AddOrder(OrderViewModel order)
		{
			var myOrder = _mapper.Map<Order>(order);
			_ordersRepo.AddOrder(myOrder); //Add Order using OrderViewModel
			return myOrder.Id;
		}

		public void DeleteOrder(OrderViewModel order)
		{
			var myOrder = _mapper.Map<Order>(order);
			_ordersRepo.DeleteOrder(myOrder); //Delete Order using OrderViewModel
		}

		public IQueryable<OrderViewModel> GetOrders(string email)
		{
			var orders = _ordersRepo.GetOrders().Where(x => x.UserEmail.Contains(email)).ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);
			return orders; //Get all orders where email = email
		}

		public OrderViewModel GetOrder(int id)
		{
			var myOrder = _ordersRepo.GetOrder(id);
			var result = _mapper.Map<OrderViewModel>(myOrder);
			return result; //Get Order using Id
		}
	}
}
