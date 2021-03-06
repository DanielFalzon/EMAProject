﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class WebView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebViewID { get; set; }
        [Required]
        public string ViewName { get; set; }

        public ICollection<GdprPolicyWebView> GdprPolicyWebViews { get; set; }
    }
}
