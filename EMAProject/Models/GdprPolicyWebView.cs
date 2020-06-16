using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Models
{
    public class GdprPolicyWebView
    {
        public int GdprPolicyID { get; set; }
        public GdprPolicy GdprPolicy { get; set; }

        public int WebViewID { get; set; }
        public WebView WebView { get; set; }

    }
}
