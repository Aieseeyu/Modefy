using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class AdminUser
    {
        [Key]
        public int AdminUserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        // foreign key to AdminRole
        [Required]
        public int RoleId { get; set; }

        // default constructor
        public AdminUser() { }

        // full constructor
        public AdminUser(int adminUserId, string firstName, string lastName, string email, string password, int roleId)
        {
            AdminUserId = adminUserId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
    }
}
