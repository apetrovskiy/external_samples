//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicBox.Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Release
    {
        public Release()
        {
            this.Tracks = new HashSet<Track>();
        }
    
        public int ReleaseID { get; set; }
        public int ArtistID { get; set; }
        public string ReleaseName { get; set; }
        public string ReleaseDate { get; set; }
        public string MusicBrainzID { get; set; }
        public string Status { get; set; }
    
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}