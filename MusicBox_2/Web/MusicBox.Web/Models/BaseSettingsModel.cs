using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBox.Web.Models
{
    public class BaseSettingsModel
    {
        public NZBProviderSettingModel ProviderModel { get; set; }
        public DownloadSettingModel DownloadModel { get; set; }
        public QualityProcessingSettingModel QualityAndProcessingModel { get; set; }
        public AdvancedSettingModel AdvancedModel { get; set; }
    }
}