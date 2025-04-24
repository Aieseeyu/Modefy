using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Refund
    {
        [Key]
        public int RefundId { get; set; }

        // foreign key to PurchaseItem
        [Required]
        public int PurchaseItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string? Reason { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // foreign key to AdminUser
        public int? AdminUserId { get; set; }

        // default constructor
        public Refund() { }

        // full constructor
        public Refund(int refundId, int purchaseItemId, int quantity, decimal amount, string? reason, DateTime date, int? adminUserId)
        {
            RefundId = refundId;
            PurchaseItemId = purchaseItemId;
            Quantity = quantity;
            Amount = amount;
            Reason = reason;
            Date = date;
            AdminUserId = adminUserId;
        }
    }
}
