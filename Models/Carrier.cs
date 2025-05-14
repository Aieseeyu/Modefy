using System;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Carrier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactPhone { get; set; }

        [Required]
        [StringLength(255)]
        public string HeadquarterAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(100)]
        public string ShippingZones { get; set; }

        [StringLength(255)]
        public string? TrackingUrl { get; set; }

        [StringLength(255)]
        public string? LogoUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // default constructor
        public Carrier() { }

        // full constructor
        public Carrier(int id, string name, string contactEmail, string contactPhone, string headquarterAddress, string country, string shippingZones, string? trackingUrl, string? logoUrl, DateTime createdAt, bool isActive)
        {
            Id = id;
            Name = name;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
            HeadquarterAddress = headquarterAddress;
            Country = country;
            ShippingZones = shippingZones;
            TrackingUrl = trackingUrl;
            LogoUrl = logoUrl;
            CreatedAt = createdAt;
            IsActive = isActive;
        }
    }
}
