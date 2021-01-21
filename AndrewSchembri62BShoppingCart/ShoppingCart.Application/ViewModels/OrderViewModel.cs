using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public DateTime DatePlaced { get; set; }
		public string UserEmail { get; set; }
	}
}
