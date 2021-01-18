using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
	class CartRepository : ICartsRepository
	{
		ShoppingCartDbContext _context;
		public CartRepository(ShoppingCartDbContext context)
		{
			_context = context;
		}

		public int AddToCart(Cart c)
		{
			_context.Carts.Add(c);
			_context.SaveChanges();
			return c.Id;
		}

		public void DeleteFromcart(Cart c)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Cart> GetCarts()
		{
			throw new NotImplementedException();
		}

		public Cart GetCart(Guid id)
		{
			throw new NotImplementedException();
		}

		public Cart GetCart(int id)
		{
			throw new NotImplementedException();
		}
		Guid ICartsRepository.AddToCart(Cart c)
		{
			throw new NotImplementedException();
		}
	}
}
