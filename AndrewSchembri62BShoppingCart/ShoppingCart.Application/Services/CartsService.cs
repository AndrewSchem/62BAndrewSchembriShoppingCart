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
			var carts = GetCarts(email);
			int found = 0;
			int cartId;
			Cart myCart = new Cart();
			Cart toDelete = new Cart();
			var myProduct = _mapper.Map<Product>(product);

			foreach (var cart in carts)
			{
				if (cart.Product.Id == myProduct.Id)
				{
					cartId = cart.Id;
					found = 1;

					toDelete = _cartsRepo.GetCart(cart.Id);
					myCart.Quantity = cart.Quantity;
					break;
				}
			}

			myCart.ProductId = myProduct.Id;
			myCart.Product = null;
			myCart.Email = email;

			if (found == 1)
			{
				_cartsRepo.DeleteFromCart(toDelete);
				myCart.Quantity = myCart.Quantity + quantity;
			}
			else
			{
				myCart.Quantity = quantity;
			}

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
			var cart = _cartsRepo.GetCarts(email).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);
			return cart;
		}

		public CartViewModel GetProduct(Guid id)
		{
			throw new NotImplementedException();
		}

	}
}
