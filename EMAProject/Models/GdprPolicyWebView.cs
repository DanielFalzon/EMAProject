﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class GdprPolicyWebView
    {
        [ForeignKey("GdprPolicy")]
        [Column(Order = 1)]
        [Required]
        public int GdprPolicyID { get; set; }
        public GdprPolicy GdprPolicy { get; set; }
        
        [ForeignKey("WebView")]
        [Column(Order = 2)]
        [Required]
        public int WebViewID { get; set; }
        public WebView WebView { get; set; }

    }
}
