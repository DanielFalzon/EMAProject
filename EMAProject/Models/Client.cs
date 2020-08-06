using EMAProject.Data;
using Microsoft.EntityFrameworkCore;
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
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
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

        [Display(Name = "Referred By")]
        public string ReferredBy { get; set; }

        [Display(Name = "Subscribe Client to Content")]
        public bool Subscriber { get; set; }
        
        [Display (Name = "Additional Client Notes")]
        public string ClientNotes { get; set; }
        
        [Display (Name = "Medications")]
        public string Medications { get; set; }

        //Significant Other Information
        [Display(Name = "First Name: ")]
        public string SoFirstName { get; set; }

        [Display(Name = "Last Name: ")]
        public string SoLastName { get; set; }

        [Display(Name = "Relationship with SO: ")]
        public string SoRelationship { get; set; }

        [Display(Name = "Contact Number: ")]
        public string SoContactNumber { get; set; }

        public List<ClientHealthCareProvider> ClientHealthcareProviders { get; set; }

        public List<ClientIntervention> ClientInterventions { get; set; }

        public void SetHealthCareProviders(ClinicContext _context) {
            this.ClientHealthcareProviders = _context.Clients.Where(c => c.ClientID == ClientID).Include(c => c.ClientHealthcareProviders).Select(c => c.ClientHealthcareProviders).SingleOrDefault().ToList();
        }

        public void ToggleHealthCareProviders(ClinicContext _context, List<int> inSelectedHcps) {
            SetHealthCareProviders(_context);

            List<int> clientHcps = this.ClientHealthcareProviders.Select(x => x.HealthCareProviderID).ToList();
            List<int> selectedHcps = inSelectedHcps;
            List<int> allHcpIds = new List<int>();

            allHcpIds.AddRange(clientHcps);
            allHcpIds.AddRange(selectedHcps);

            foreach (int hcpId in allHcpIds.Distinct())
            {
                if (!clientHcps.Contains(hcpId) && selectedHcps.Contains(hcpId))
                {
                    _context.Add(new ClientHealthCareProvider()
                    {
                        ClientID = ClientID,
                        HealthCareProviderID = hcpId
                    });
                }
                else
                {
                    if (clientHcps.Contains(hcpId) && !selectedHcps.Contains(hcpId))
                    {
                        ClientHealthCareProvider chcpToDelete = _context.ClientHealthCareProviders.FirstOrDefault(chcp => chcp.ClientID == ClientID && chcp.HealthCareProviderID == hcpId);
                        _context.ClientHealthCareProviders.Remove(chcpToDelete);
                    }
                }
            }
        }
    }
}
