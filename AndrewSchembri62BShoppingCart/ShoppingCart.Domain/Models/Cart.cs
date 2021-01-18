using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
	public class Cart
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual Product Product{ get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
