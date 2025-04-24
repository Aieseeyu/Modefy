using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        // foreign key to Customer
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(110)]
        public string CustomerFullName { get; set; }

        [Required]
        [StringLength(255)]
        public string BillingAddress { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(255)]
        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [StringLength(100)]
        public string? StripeId { get; set; }

        // foreign key to Promotion
        public int? PromotionId { get; set; }

        [StringLength(30)]
        public string? PromotionCode { get; set; }

        public decimal DiscountAmount { get; set; } = 0.00M;

        // default constructor
        public Purchase() { }

        // full constructor
        public Purchase(int purchaseId, int customerId, DateTime date, string customerFullName, string billingAddress, string status, decimal totalAmount, string shippingAddress, string paymentMethod, string? stripeId, int? promotionId, string? promotionCode, decimal discountAmount)
        {
            PurchaseId = purchaseId;
            CustomerId = customerId;
            Date = date;
            CustomerFullName = customerFullName;
            BillingAddress = billingAddress;
            Status = status;
            TotalAmount = totalAmount;
            ShippingAddress = shippingAddress;
            PaymentMethod = paymentMethod;
            StripeId = stripeId;
            PromotionId = promotionId;
            PromotionCode = promotionCode;
            DiscountAmount = discountAmount;
        }
    }
}

