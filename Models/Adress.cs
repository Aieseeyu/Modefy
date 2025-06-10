using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        // foreign key to Customer
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        public string Line1 { get; set; }

        [StringLength(255)]
        public string? Line2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        // default constructor
        public Address() { }

        // full constructor
        public Address(int addressId, int customerId, string line, string? line2, string city, string postalCode, string country, string type)
        {
            AddressId = addressId;
            CustomerId = customerId;
            Line1 = line;
            Line2 = line2;
            City = city;
            PostalCode = postalCode;
            Country = country;
            Type = type;
        }
    }
}
