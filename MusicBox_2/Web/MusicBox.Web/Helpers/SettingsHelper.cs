using MusicBox.Framework.Common;
using MusicBox.Web.Data;
using MusicBox.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBox.Web.Helpers
{
    public static class SettingsHelper
    {
        public static BaseSettingsModel BuildSettingsFile(musicBoxDBEntities db)
        {
            BaseSettingsModel settings = new BaseSettingsModel();

            settings.AdvancedModel = new AdvancedSettingModel();
            settings.DownloadModel = new DownloadSettingModel();
            settings.ProviderModel = new NZBProviderSettingModel();
            settings.QualityAndProcessingModel = new QualityProcessingSettingModel();

            BuildUpAdvancedSettings(db, settings.AdvancedModel);
            BuildUpDownloadSettings(db, settings.DownloadModel);
            BuildUpProviderSettings(db, settings.ProviderModel);
            BuildUpQualitySettings(db, settings.QualityAndProcessingModel);

            return settings;
        }

        public static string GetSetting(string key, musicBoxDBEntities db)
        {
            var setting = db.Settings.Where(s => s.SettingKey.Equals(key)).FirstOrDefault();

            if (setting == null) return string.Empty;

            return setting.SettingValue;
        }

        private static void BuildUpAdvancedSettings(musicBoxDBEntities db, AdvancedSettingModel model)
        {

        }

        private static void BuildUpDownloadSettings(musicBoxDBEntities db, DownloadSettingModel model)
        {
            model.SABApiKey = GetValue(Constants.SettingsKey.Download.SABApiKey, string.Empty, db);
            model.SABCategory = GetValue(Constants.SettingsKey.Download.SABCategory, "Default", db);
            model.SABCompleteFolder = GetValue(Constants.SettingsKey.Download.SABCompleteFolder, "", db);
            model.SABNZBDUrl = GetValue(Constants.SettingsKey.Download.SABNZBDUrl, "", db);
        }

        private static void BuildUpProviderSettings(musicBoxDBEntities db, NZBProviderSettingModel model)
        {
            model.APIKey = GetValue(Constants.SettingsKey.Provider.APIKey, "", db);
            model.NewsnabUrl = GetValue(Constants.SettingsKey.Provider.NewsNabUrl, "", db);
        }

        private static void BuildUpQualitySettings(musicBoxDBEntities db, QualityProcessingSettingModel model)
        {
            model.CorrectMetadata = bool.Parse(GetValue(Constants.SettingsKey.QualityAndProcessing.CorrectMetadata, bool.FalseString, db));
            model.DeleteLeftovers = bool.Parse(GetValue(Constants.SettingsKey.QualityAndProcessing.DeleteLeftovers, bool.FalseString, db));
            model.DestinationPath = GetValue(Constants.SettingsKey.QualityAndProcessing.DestinationPath, "",db);
            model.MoveDownloads = bool.Parse(GetValue(Constants.SettingsKey.QualityAndProcessing.MoveDownloads, bool.FalseString, db));
            model.RenameFiles = bool.Parse(GetValue(Constants.SettingsKey.QualityAndProcessing.RenameFiles, bool.FalseString, db));
        }

        private static string GetValue(string key, string defaultValue, musicBoxDBEntities db)
        {
            string value = "";

            var setting = db.Settings.Where(s => s.SettingKey.Equals(key)).FirstOrDefault();

            if (setting == null)
            {
                db.Settings.Add(CreateSetting(key, defaultValue));
                db.SaveChanges();
                value = bool.FalseString;
            }
            else
                value = setting.SettingValue;

            return value;
        }

        private static Setting CreateSetting(string key, string value)
        {
            return new Setting() { SettingKey = key, SettingValue = value };
        }
    }
}