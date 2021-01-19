using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepository
    {
        IQueryable<Cart> GetCarts();
        IQueryable<Cart> GetCarts(string email);
        Cart GetCart(int id);
        void DeleteFromCart(Cart c);
        int AddToCart(Cart c);
        int EditCart(Cart c);
    }
}
