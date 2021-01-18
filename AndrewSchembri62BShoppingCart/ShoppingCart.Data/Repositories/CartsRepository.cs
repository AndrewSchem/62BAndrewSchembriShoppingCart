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

		public IQueryable<Cart> GetCarts()
		{
			return _context.Carts;
		}

		public Cart GetCart(int id)
		{
			return _context.Carts.SingleOrDefault(x => x.Id == id);
		}
		Guid ICartsRepository.AddToCart(Cart c)
		{
			throw new NotImplementedException();
		}
	}
}
