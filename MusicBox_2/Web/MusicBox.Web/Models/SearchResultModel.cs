using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBox.Web.Models
{
    public class SearchResultModel
    {
        public string Name { get; set; }
        public string MBID { get; set; }
        public string CoverArtURL { get; set; }
    }
}