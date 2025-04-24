using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModefyEcommerce.Models
{
    public class VariationOption
    {
        [Key]
        public int VariationOptionId { get; set; }

        // foreign key to Variation
        [Required]
        public int VariationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        // default constructor
        public VariationOption() { }

        // full constructor
        public VariationOption(int variationOptionId, int variationId, string value)
        {
            VariationOptionId = variationOptionId;
            VariationId = variationId;
            Value = value;
        }
    }
}
