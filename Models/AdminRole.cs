using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class AdminRole
    {
        [Key]
        public int AdminRoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Permissions { get; set; }

        // default constructor
        public AdminRole() { }

        // full constructor
        public AdminRole(int adminRoleId, string name, string permissions)
        {
            AdminRoleId = adminRoleId;
            Name = name;
            Permissions = permissions;
        }
    }
}
