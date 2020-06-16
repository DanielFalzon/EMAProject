using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class GdprPolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GdprPolicyID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public ICollection<GdprPolicyWebView> GdprPolicyWebViews { get; set; }
    }
}
