using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }


        public virtual Product Product { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }


        public virtual Order Order { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

    }
}
