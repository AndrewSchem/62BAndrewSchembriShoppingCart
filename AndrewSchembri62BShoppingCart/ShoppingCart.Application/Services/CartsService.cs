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
			var carts = GetCarts(email); //Get All Carts Using Email
			int found = 0;
			int cartId;
			Cart myCart = new Cart();
			Cart toDelete = new Cart();
			var myProduct = _mapper.Map<Product>(product);

			foreach (var cart in carts)
			{
				if (cart.Product.Id == myProduct.Id) //If Product is in cart
				{
					cartId = cart.Id;
					found = 1;

					toDelete = _cartsRepo.GetCart(cart.Id); //Get Product to Delete From Cart if Found
					myCart.Quantity = cart.Quantity;
					break;
				}
			}

			myCart.ProductId = myProduct.Id;
			myCart.Product = null;
			myCart.Email = email;

			if (found == 1) //If Item found in cart
			{
				_cartsRepo.DeleteFromCart(toDelete); //Delete From Cart Product Found
				myCart.Quantity = myCart.Quantity + quantity; //Add Quantity to Item to Add to Cart
			}
			else
			{
				myCart.Quantity = quantity; //If Item Not Found Add With Wanted Quantity
			}

			_cartsRepo.AddToCart(myCart); //Add Product to My Cart
		}

		public void DeleteFromCart(int id)
		{
			var cartToDelete = _cartsRepo.GetCart(id); //Get Cart to Delete

			if (cartToDelete != null) //If Cart is Not Null
			{
				_cartsRepo.DeleteFromCart(cartToDelete); //Delete Cart
			}
		}


		public IQueryable<CartViewModel> GetCarts()
		{
			var carts = _cartsRepo.GetCarts().ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);
			return carts; //Get All Carts
		}

		public CartViewModel GetCart(int id)
		{
			var myCart = _cartsRepo.GetCart(id);
			var result = _mapper.Map<CartViewModel>(myCart);
			return result; //Get Cart with Id
		}

		//https://stackoverflow.com/questions/2113498/sqlexception-from-entity-framework-new-transaction-is-not-allowed-because-ther/2180920#2180920
		//Using IList to Solve Error (Multiple Connections Ongoing??)
		public IList<CartViewModel> GetCarts(string email)
		{
			var cart = _cartsRepo.GetCarts(email).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);
			return cart.ToList(); //Get Cart with Email
		}

	}
}
