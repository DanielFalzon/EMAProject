using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class Intervention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterventionID { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal PreInterventionScore { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal PostInterventionScore { get; set; }

        public string Antecedence { get; set; }

        [Display(Name = "Behaviours to Address")]
        public string Behaviours { get; set; }

        public string Consequence { get; set; }

        public string Treatment { get; set; }

        public ICollection<ClientIntervention> ClientInterventions {get; set;}

        public ICollection<Session> Sessions { get; set; }
    }
}
