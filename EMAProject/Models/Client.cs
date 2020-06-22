using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }

        [Required]
        [Display(Name = "NI/ID Card Number")]
        public string NiNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "The E-Mail address is not valid.")]
        [Display(Name = "Contact E-Mail Address")]
        public string Email { get; set; }

        [Display(Name = "House/Block Number and/or Name")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Road Name")]
        public string AddressLine2 { get; set; }
        
        [Display(Name = "Locality")]
        public string AddressLine3 { get; set; }

        [Display(Name = "ReferredBy")]
        public string ReferredBy { get; set; }

        [Display(Name = "Subscribe Client to Content")]
        public bool Subscriber { get; set; }
        
        [Display (Name = "Additional Client Notes")]
        public string ClientNotes { get; set; }
        
        [Display (Name = "Medications")]
        public string Medications { get; set; }

        //Significant Other Information
        [Display(Name = "SO: ")]
        public string SoFirstName { get; set; }

        [Display(Name = "SO: ")]
        public string SoLastName { get; set; }

        [Display(Name = "SO: ")]
        public string SoRelationship { get; set; }

        [Display(Name = "SO: ")]
        public string SoContactNumber { get; set; }

        public ICollection<ClientHealthCareProvider> ClientHealthcareProviders;
    }
}
