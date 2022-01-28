using GUI.Logger;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Controls;

namespace GUI
{
    class SettingsHelper
    {
        private const string FILE_NAME = "settings.json";
        public static void LoadJson(ViewModel model, ComboBox comboBox)
        {
            try
            {
                if (!File.Exists(FILE_NAME))
                {
                    FileLogger.LogWarning($"Config file {FILE_NAME} not found...");
                    return;
                }

                FileLogger.LogWarning($"Reading Config from {FILE_NAME} file");
                var serializer = new JsonSerializer();
                using (var s = File.Open(FILE_NAME, FileMode.Open))
                using (var sr = new StreamReader(s))
                using (var reader = new JsonTextReader(sr))
                {
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            var data = serializer.Deserialize<ViewModel>(reader);

                            model.IpAddress = string.IsNullOrEmpty(data.IpAddress) ? model.IpAddress : data.IpAddress;
                            model.IpPort = string.IsNullOrEmpty(data.IpPort) ? model.IpPort : data.IpPort;
                            model.Bandwidth = string.IsNullOrEmpty(data.Bandwidth) ? "9600" : data.Bandwidth;
                            comboBox.SelectedIndex = string.IsNullOrEmpty(data.AppId) ? 0 : Convert.ToInt32(data.AppId);
                        }
                    }
                }

                FileLogger.LogWarning($"Successfully loaded config values");
            }
            catch (Exception ex)
            {
                FileLogger.LogWarning($"Problem with {FILE_NAME} occured\n{ex.Message}");
            }
        }
    }
}
