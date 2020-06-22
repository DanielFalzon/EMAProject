using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class Session
    {
        public enum PersonEntities
        { 
            Client,
            AccompanyingClient,
            Therapist
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionID { get; set; }

        [Display(Name = "Session Due On")]
        public DateTime SessionTime { get; set; }

        [Display(Name = "Accompanying Client Attended" )]
        public bool IsAccompanied { get; set; }

        public bool IsDelivered { get; set; }

        public PersonEntities CancelledBy { get; set; }

        [ForeignKey("Intervention")]
        public int InterventionID { get; set; }
        public virtual Intervention Intervention { get; set; }

        public virtual SessionNote SessionNote { get; set; }
    }
}
