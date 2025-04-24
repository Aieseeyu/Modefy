using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Variation
    {
        [Key]
        public int VariationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Label { get; set; }

        // default constructor
        public Variation() { }

        // full constructor
        public Variation(int variationId, string label)
        {
            VariationId = variationId;
            Label = label;
        }
    }
}
