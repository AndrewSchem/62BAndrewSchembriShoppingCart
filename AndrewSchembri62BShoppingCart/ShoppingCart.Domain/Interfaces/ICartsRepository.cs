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
        Cart GetCart(int id);

        void DeleteFromcart(Cart c);

        Guid AddToCart(Cart c);


    }
}
