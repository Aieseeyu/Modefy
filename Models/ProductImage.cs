using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }

        // foreign key to Product
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        // default constructor
        public ProductImage() { }

        // full constructor
        public ProductImage(int productImageId, int productId, string url)
        {
            ProductImageId = productImageId;
            ProductId = productId;
            Url = url;
        }
    }
}

