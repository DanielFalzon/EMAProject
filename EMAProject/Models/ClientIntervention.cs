using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class ClientIntervention
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Client")]
        [Required]
        public int ClientID { get; set; }
        public Client Client { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Intervention")]
        [Required]
        public int InterventionID { get; set; }
        public Intervention Intervention { get; set; }
    }
}
