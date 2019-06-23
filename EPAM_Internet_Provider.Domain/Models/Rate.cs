using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Internet_Provider.Domain.Models
{
    public class Rate
    {
        /// <summary>
        /// Model of rate
        /// </summary>
        [Key]
        public int RateId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Rate name")]
        public string RateName { get; set; }

        [Required]
        [Display(Name = "Rate cost")]
        public decimal RateCost { get; set; }
    }
}
