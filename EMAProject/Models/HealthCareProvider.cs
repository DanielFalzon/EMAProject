using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class HealthCareProvider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HealthCareProviderID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        public ICollection<ClientHealthCareProvider> ClientHealthCareProviders { get; set; }
    }
}
