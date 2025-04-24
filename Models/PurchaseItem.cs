using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class PurchaseItem
    {
        [Key]
        public int PurchaseItemId { get; set; }

        // foreign key to Purchase
        [Required]
        public int PurchaseId { get; set; }

        // foreign key to ProductVariant
        [Required]
        public int ProductVariantId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(30)]
        public string? Color { get; set; }

        [StringLength(10)]
        public string? Size { get; set; }

        [StringLength(30)]
        public string? Material { get; set; }

        [StringLength(50)]
        public string? ProductReference { get; set; }

        // default constructor
        public PurchaseItem() { }

        // full constructor
        public PurchaseItem(int purchaseItemId, int purchaseId, int productVariantId, int quantity, decimal unitPrice, string productName, string? color, string? size, string? material, string? productReference)
        {
            PurchaseItemId = purchaseItemId;
            PurchaseId = purchaseId;
            ProductVariantId = productVariantId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductName = productName;
            Color = color;
            Size = size;
            Material = material;
            ProductReference = productReference;
        }
    }
}

