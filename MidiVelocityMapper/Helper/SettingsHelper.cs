using MidiVelocityMapper.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MidiVelocityMapper.Helper
{
    public class Settings
    {
        public string MidiIn { get; set; }
        public string MidiOut { get; set; }
        public string SelectedCurve { get; set; }
        public IList<VelocityCurve> Curves { get; set; }
    }

    public class VelocityCurve
    {
        public string Name { get; set; }
        public IList<VelocityMap> Map { get; set; }
    }

    public class SettingsHelper
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _settingsFile;

        public SettingsHelper(string settingsFile, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _settingsFile = settingsFile;
        }

        public SettingsHelper(string settingsFile)
            : this(settingsFile, new FileSystem())
        {
        }

        public string Serialize(Settings settings)
        {
            var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(settings, serializerOptions);
            return jsonString;
        }

        public Settings DeSerialize(string jsonString)
        {
            var settings = JsonSerializer.Deserialize<Settings>(jsonString);
            return settings;
        }

        public Settings GetSettings()
        {
            var jsonString = _fileSystem.File.ReadAllText(_settingsFile);
            return DeSerialize(jsonString);
        }

        public void SaveSettings(Settings settings)
        {
            var jsonString = Serialize(settings);
            _fileSystem.File.WriteAllText(_settingsFile, jsonString);
        }
    }
}
