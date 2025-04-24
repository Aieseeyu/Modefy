using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Le nom du produit est requis.")]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Le statut du produit est requis.")]
        [StringLength(20)]
        public string ProductStatus { get; set; }

        [Required]
        public DateTime ProductCreatedAt { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }

        public Product()
        {
        }

        public Product(int productId, string productName, string productDescription, string productStatus, DateTime productCreatedAt, int productCategoryId)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductStatus = productStatus;
            ProductCreatedAt = productCreatedAt;
            ProductCategoryId = productCategoryId;
        }
    }
}

