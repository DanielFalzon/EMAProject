using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Display(Name = "Session Notes")]
        public string PreSessionNotes { get; set; }

        [Display(Name = "Session Due On")]
        public DateTime SessionTime { get; set; }

        [Display(Name = "Accompanying Client Attended")]
        public bool IsAccompanied { get; set; } = false;

        public PersonEntities? CancelledBy { get; set; }

        [ForeignKey("Intervention")]
        public int InterventionID { get; set; }
        public Intervention Intervention { get; set; }
        
        [ForeignKey("SessionNote")]
        public int? SessionNoteID { get; set; }
        public SessionNote SessionNote { get; set; }

        [NotMapped]
        public bool IsDelivered
        {
            get
            {
                return SessionNoteID != null;
            }
        }


        [NotMapped]
        [Display(Name = "File")]
        [BindProperty]
        public IFormFile FormFile { get; set; }
    }
}
