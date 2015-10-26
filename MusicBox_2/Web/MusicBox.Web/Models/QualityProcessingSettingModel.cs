using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBox.Web.Models
{
    public class QualityProcessingSettingModel
    {
        public bool MoveDownloads { get; set; }
        public bool RenameFiles { get; set; }
        public bool CorrectMetadata { get; set; }
        public bool DeleteLeftovers { get; set; }
        public string DestinationPath { get; set; }
    }
}