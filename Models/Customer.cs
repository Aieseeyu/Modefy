using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public byte Sex { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        // default constructor
        public Customer() { }

        // full constructor
        public Customer(int customerId, string firstName, string lastName, byte sex, string email, string password, string phoneNumber, DateTime createdAt, string status)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
            Status = status;
        }
    }
}
