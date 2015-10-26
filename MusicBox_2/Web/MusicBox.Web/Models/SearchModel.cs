using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicBox.Web.Models
{
    public class SearchModel
    {
        [Required]
        [Display(Name = "Search Term")]
        public string SearchTerm { get; set; }
    }
}