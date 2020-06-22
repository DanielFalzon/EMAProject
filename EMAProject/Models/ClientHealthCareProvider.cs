using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class ClientHealthCareProvider
    {
        [Key]
        [ForeignKey("HealthCareProvider")]
        public int HealthCareProviderID { get; set; }
        public HealthCareProvider HealthCareProvider { get; set; }

        [Key]
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        public Client Client { get; set; }
    }
}
