using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPAM_Internet_Provider.Domain.Models
{
    public class User
    {
        public User()
        {
            this.Subscributions = new HashSet<Subscription>();
        }

        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public decimal Account { get; set; }

        [Required]
        public string Role { get; set; }

        public virtual ICollection<Subscription> Subscributions { get; set; }
    }
}
