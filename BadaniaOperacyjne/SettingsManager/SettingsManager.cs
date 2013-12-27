using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BadaniaOperacyjne.DataType;

namespace BadaniaOperacyjne.SettingsManager
{
    class SettingsManager : ISettingsManager
    {
        private static string filePath = "settings.dat";

        private static Settings defaultSettings = new Settings
        {
            StartingTemperature = 100,
            EndingTemperature = 50,
            NumIterations = 1000,
            NumIterationsMultiplier = 1,
            CoolingCoefficient = 0.9
        };

        private static Settings currentSettings = null;

        public void SetSettings(Settings settings)
        {
            FileStream stream = null;
            BinaryWriter writer = null;

            try
            {
                stream = new FileStream(filePath, FileMode.Create);
                writer = new BinaryWriter(stream);

                writer.Write(settings.StartingTemperature);
                writer.Write(settings.EndingTemperature);
                writer.Write(settings.NumIterations);
                writer.Write(settings.NumIterationsMultiplier);
                writer.Write(settings.CoolingCoefficient);

                currentSettings = settings;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (writer != null)
                    writer.Close();
            }
        }

        public Settings GetSettings()
        {
            if (currentSettings != null)
                return currentSettings;

            FileStream stream = null;
            BinaryReader reader = null;
            try
            {
                Settings settings = new Settings();
                stream = new FileStream(filePath, FileMode.Open);
                reader = new BinaryReader(stream);

                settings.StartingTemperature = reader.ReadDouble();
                settings.EndingTemperature = reader.ReadDouble();
                settings.NumIterations = reader.ReadInt32();
                settings.NumIterationsMultiplier = reader.ReadDouble();
                settings.CoolingCoefficient = reader.ReadDouble();

                currentSettings = settings;
                return currentSettings;
            }
            catch
            {
                return defaultSettings;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
