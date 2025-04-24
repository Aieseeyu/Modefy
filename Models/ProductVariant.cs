using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class ProductVariant
    {
        [Key]
        public int ProductVariantId { get; set; }

        // foreign key to Product
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "SKU must not exceed 50 characters.")]
        public string Sku { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        // default constructor
        public ProductVariant() { }

        // full constructor
        public ProductVariant(int productVariantId, int productId, string sku, decimal price, int stock)
        {
            ProductVariantId = productVariantId;
            ProductId = productId;
            Sku = sku;
            Price = price;
            Stock = stock;
        }
    }
}
