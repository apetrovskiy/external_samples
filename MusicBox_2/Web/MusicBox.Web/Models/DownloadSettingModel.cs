using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBox.Web.Models
{
    public class DownloadSettingModel
    {
        public string SABNZBDUrl { get; set; }
        public string SABApiKey { get; set; }
        public string SABCategory { get; set; }
        public string SABCompleteFolder { get; set; }

    }
}