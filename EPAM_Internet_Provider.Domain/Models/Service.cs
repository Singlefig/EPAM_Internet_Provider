using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EPAM_Internet_Provider.Domain.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public ServicesType ServiceType { get; set; }

        [Required]
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
