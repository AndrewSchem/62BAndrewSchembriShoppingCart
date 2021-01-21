using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
	public class OrderDetailsViewModel
	{
        public int Id { get; set; }

        public ProductViewModel Product { get; set; }

        public OrderViewModel Order { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
