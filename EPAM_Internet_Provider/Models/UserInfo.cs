using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.Models
{
    public class UserInfo
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public decimal Account { get; set; }

        [Required]
        public string Role { get; set; }

        public ICollection<Subscription> Subscributions { get; set; }
    }
}