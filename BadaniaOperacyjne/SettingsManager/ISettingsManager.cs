using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;

namespace BadaniaOperacyjne.SettingsManager
{
    public interface ISettingsManager
    {
        void SetSettings(Settings settings);

        Settings GetSettings();
    }
}
