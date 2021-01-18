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

		public void AddToCart(ProductViewModel product, int quantity, string email)
		{
			Cart myCart = new Cart();
			var myProduct = _mapper.Map<Product>(product);
			myCart.Product = myProduct;
			myCart.Quantity = quantity;
			myCart.Email = email;

			_cartsRepo.AddToCart(myCart);
		}

		public void DeleteFromCart(int id)
		{
			var cartToDelete = _cartsRepo.GetCart(id);

			if (cartToDelete != null)
			{
				_cartsRepo.DeleteFromCart(cartToDelete);
			}
		}


		public IQueryable<CartViewModel> GetCarts()
		{
			var carts = _cartsRepo.GetCarts().ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);
			return carts;
		}

		public CartViewModel GetCart(int id)
		{
			var myCart = _cartsRepo.GetCart(id);
			var result = _mapper.Map<CartViewModel>(myCart);
			return result;
		}

		public IQueryable<CartViewModel> GetCarts(string email)
		{
			var cart = _cartsRepo.GetCarts().Where(x => x.Email.Contains(email)).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);
			return cart;
		}

		public CartViewModel GetProduct(Guid id)
		{
			throw new NotImplementedException();
		}

	}
}
