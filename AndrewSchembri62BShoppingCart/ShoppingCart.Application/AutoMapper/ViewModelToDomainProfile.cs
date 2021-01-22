using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{

    public class ViewModelToDomainProfile: Profile
    {
        public ViewModelToDomainProfile()
        {
            //When changing from ProductViewModel to Product
            CreateMap<ProductViewModel, Product>();
            CreateMap<OrderViewModel, Order>();
            CreateMap<OrderDetailsViewModel, OrderDetails>();
            CreateMap<CartViewModel,Cart>();
            CreateMap<CategoryViewModel, Category>();
            CreateMap<MemberViewModel, Member>();
        }
    }
}
