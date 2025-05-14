using System;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        // foreign key to Customer
        [Required]
        public int CustomerId { get; set; }

        // foreign key to AdminUser (nullable)
        public int? AdminUserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsClosed { get; set; } = false;

        // default constructor
        public Chat() { }

        // full constructor
        public Chat(int id, int customerId, int? adminUserId, DateTime createdAt, bool isClosed)
        {
            Id = id;
            CustomerId = customerId;
            AdminUserId = adminUserId;
            CreatedAt = createdAt;
            IsClosed = isClosed;
        }
    }
}
