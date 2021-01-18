using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
	public class CartsService : ICartsService
	{
		private ICartsRepository _cartsRepo;

		private IMapper _mapper;
		public CartsService(ICartsRepository cartsRepository, IMapper mapper)
		{
			_mapper = mapper;
			_cartsRepo = cartsRepository;
		}
		//public CartsService(ICartsRepository cartsRepository)
		//{
			//_cartsRepo = cartsRepository;
		//}
		public void AddToCart(ProductViewModel product)
		{
			throw new NotImplementedException();
		}

		public void DeleteFromCart(Guid id)
		{
			throw new NotImplementedException();
		}


		public IQueryable<CartViewModel> GetCarts()
		{
			var carts = _cartsRepo.GetCarts().ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);
			return carts;
		}

		public CartViewModel GetCart(Guid id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<CartViewModel> GetCarts(string email)
		{
			throw new NotImplementedException();
		}

		public CartViewModel GetProduct(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
