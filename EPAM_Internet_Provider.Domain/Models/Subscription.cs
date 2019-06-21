using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_Internet_Provider.Domain.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [Required]
        public virtual Service Service { get; set; }

        [Required]
        public virtual Rate SubscriptionRate { get; set; }

        [Required]
        public decimal ServiceBalance { get; set; }

        [Required]
        public bool IsBlocked { get; set; }
    }
}
