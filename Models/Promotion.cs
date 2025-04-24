using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        [StringLength(10)]
        public string Unit { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(30)]
        public string? Code { get; set; }

        // default constructor
        public Promotion() { }

        // full constructor
        public Promotion(int promotionId, string name, string type, decimal value, string unit, DateTime startDate, DateTime? endDate, string? code)
        {
            PromotionId = promotionId;
            Name = name;
            Type = type;
            Value = value;
            Unit = unit;
            StartDate = startDate;
            EndDate = endDate;
            Code = code;
        }
    }
}
