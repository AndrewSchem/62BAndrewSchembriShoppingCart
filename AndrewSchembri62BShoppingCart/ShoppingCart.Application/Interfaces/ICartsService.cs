using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsService
    {
        IQueryable<CartViewModel> GetCarts();

        IQueryable<CartViewModel> GetCarts(string email);

        CartViewModel GetCart(Guid id);

        void AddToCart(ProductViewModel product);

        void DeleteFromCart(Guid id);


    }
}
