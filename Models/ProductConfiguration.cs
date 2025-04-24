using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    // association table
    public class ProductConfiguration
    {
        [Key]
        public int ProductConfigurationId { get; set; }

        // foreign key to ProductVariant
        [Required]
        public int ProductVariantId { get; set; }

        // foreign key to VariationOption
        [Required]
        public int VariationOptionId { get; set; }

        // default constructor
        public ProductConfiguration() { }

        // full constructor
        public ProductConfiguration(int productConfigurationId, int productVariantId, int variationOptionId)
        {
            ProductConfigurationId = productConfigurationId;
            ProductVariantId = productVariantId;
            VariationOptionId = variationOptionId;
        }
    }
}
