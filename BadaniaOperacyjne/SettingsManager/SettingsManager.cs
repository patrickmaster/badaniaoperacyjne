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
            StartingTemperature = 10,
            EndingTemperature = 1,
            NumIterations = 2000,
            NumIterationsMultiplier = 1.5,
            CoolingCoefficient = 0.85,
            Operation = OperationType.Operation1
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
                writer.Write((int)settings.Operation);

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
            //if (currentSettings != null)
            //    return currentSettings;

            Settings settings = null;

            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    try
                    {
                        settings = ReadSettings(reader);
                        currentSettings = settings;
                    }
                    catch
                    {
                        return defaultSettings;
                    }
                }
            }

            return settings;
        }

        private static Settings ReadSettings(BinaryReader reader)
        {
            Settings settings = new Settings();
            settings.StartingTemperature = reader.ReadDouble();
            settings.EndingTemperature = reader.ReadDouble();
            settings.NumIterations = reader.ReadInt32();
            settings.NumIterationsMultiplier = reader.ReadDouble();
            settings.CoolingCoefficient = reader.ReadDouble();
            settings.Operation = (OperationType)reader.ReadInt32();
            return settings;
        }
    }
}
