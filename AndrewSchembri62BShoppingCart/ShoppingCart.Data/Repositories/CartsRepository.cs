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
			return _context.Carts;
		}

		public IQueryable<Cart> GetCarts(string email)
		{
			var carts = _context.Carts.Where(x => x.Email.Contains(email));
			return carts;
		}

		public Cart GetCart(int id)
		{
			return _context.Carts.SingleOrDefault(x => x.Id == id);
		}

		public int EditCart(Cart c)
		{
			Cart toChange = _context.Carts.Single(x => x.Id == c.Id);
			toChange.Quantity = 10;
			_context.SaveChanges();
			return c.Id;
		}

		public int AddToCart(Cart c)
		{
			_context.Carts.Add(c);
			_context.SaveChanges();
			return c.Id;
		}

		public void DeleteFromCart(Cart c)
		{
			_context.Carts.Remove(c);
			_context.SaveChanges();
		}
	}
}
