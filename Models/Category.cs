﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(255)]
        public string? CategoryDescription { get; set; }

        // foreign key to parent Category
        public int? ParentCategoryId { get; set; }

        // default constructor
        public Category() { }

        // full constructor
        public Category(int categoryId, string name, string? description, int? parentCategoryId)
        {
            CategoryId = categoryId;
            CategoryName = name;
            CategoryDescription = description;
            ParentCategoryId = parentCategoryId;
        }
    }
}

