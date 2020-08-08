using EMAProject.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        [Display(Name = "Pre-Intervention Score")]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal PreInterventionScore { get; set; }

        [Display(Name = "Post-Intervention Score")]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal PostInterventionScore { get; set; }

        public string Antecedence { get; set; }

        [Display(Name = "Behaviours to Address")]
        public string Behaviours { get; set; }

        public string Consequence { get; set; }

        public string Treatment { get; set; }

        //D,F, Calculated Properties
        
        [NotMapped]
        [Display(Name = "Additional Client (Optional)")]
        public string AdditionalClientID { get; set; }
        //https://stackoverflow.com/questions/23413296/calculated-property-with-linq-expression
        [NotMapped]
        public DateTime FirstSession { get; set; } = DateTime.Now;
        [NotMapped]
        public DateTime NextSession { get; set; } = DateTime.Now;
        [NotMapped]
        public int AttendedSessionCount { get; set; } = 0;
        [NotMapped]
        public int CancelledSessionCouunt { get; set; } = 0;
        [NotMapped]
        public bool IsComplete { 
            get {
                return Treatment != null && Decimal.Compare(PostInterventionScore, 0.00m) > 0;
            } 
        }

        public ICollection<ClientIntervention> ClientInterventions {get; set;} = new List<ClientIntervention>();

        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        public void CreateClientInterventionLinks(ClinicContext _context, Client client) {
            List<ClientIntervention> clientInterventions = new List<ClientIntervention>();
            clientInterventions.Add(
                new ClientIntervention()
                {
                    ClientID = client.ClientID,
                    InterventionID = InterventionID
                }
            );

            int additionalClientID;
            int.TryParse(AdditionalClientID, out additionalClientID);

            if (additionalClientID > 0) {
                clientInterventions.Add(
                    new ClientIntervention()
                    {
                        ClientID = additionalClientID,
                        InterventionID = InterventionID
                    }
                );
            }

            _context.ClientInterventions.AddRange(clientInterventions);
            _context.SaveChanges();
        }
    }
}
