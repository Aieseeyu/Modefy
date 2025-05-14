using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class DeliveryType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DelayDays { get; set; }

        // foreign key to Carrier
        [Required]
        public int CarrierId { get; set; }

        // default constructor
        public DeliveryType() { }

        // full constructor
        public DeliveryType(int id, string name, string? description, decimal price, int delayDays, int carrierId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DelayDays = delayDays;
            CarrierId = carrierId;
        }
    }
}
