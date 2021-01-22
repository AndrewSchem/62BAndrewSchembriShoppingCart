using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    public class DomainToViewModelProfile: Profile
    {
        public DomainToViewModelProfile()
        {
            //When changing from Product to ProductViewModel
            CreateMap<Product, ProductViewModel>();
            CreateMap<Cart, CartViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderDetails, OrderDetailsViewModel>();
            CreateMap<Member, MemberViewModel>();

        }

    }
}
