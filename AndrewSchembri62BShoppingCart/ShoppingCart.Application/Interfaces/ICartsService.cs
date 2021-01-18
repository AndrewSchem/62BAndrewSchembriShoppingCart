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

        CartViewModel GetCart(int id);

        void AddToCart(ProductViewModel product, int quantity, String email);

        void DeleteFromCart(int id);

    }
}
