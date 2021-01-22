using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
	public class CartsRepository : ICartsRepository
	{
		ShoppingCartDbContext _context;
		public CartsRepository(ShoppingCartDbContext context)
		{
			_context = context;
		}

		public IQueryable<Cart> GetCarts()
		{
			return _context.Carts; //Return All Carts
		}

		public IQueryable<Cart> GetCarts(string email)
		{
			var carts = _context.Carts.Where(x => x.Email.Contains(email)); //Return Carts Were Email
			return carts;
		}

		public Cart GetCart(int id)
		{
			return _context.Carts.SingleOrDefault(x => x.Id == id); //Return Cart With Id
		}

		public int EditCart(Cart c)
		{
			Cart toChange = _context.Carts.Single(x => x.Id == c.Id); //Method Used For Testing
			_context.SaveChanges();
			return c.Id;
		}

		public int AddToCart(Cart c)
		{
			_context.Carts.Add(c); //Add Cart
			_context.SaveChanges();
			return c.Id;
		}

		public void DeleteFromCart(Cart c)
		{
			_context.Carts.Remove(c); //Remove Cart
			_context.SaveChanges();
		}
	}
}
