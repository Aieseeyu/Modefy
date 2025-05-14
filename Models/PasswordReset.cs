using System;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class PasswordReset
    {
        [Key]
        public int Id { get; set; }

        // foreign key to Customer
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        public string Token { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public bool Used { get; set; } = false;

        // default constructor
        public PasswordReset() { }

        // full constructor
        public PasswordReset(int id, int customerId, string token, DateTime expiration, bool used)
        {
            Id = id;
            CustomerId = customerId;
            Token = token;
            Expiration = expiration;
            Used = used;
        }
    }
}
