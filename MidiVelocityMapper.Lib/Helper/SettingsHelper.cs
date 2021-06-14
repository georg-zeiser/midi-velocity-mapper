using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MidiVelocityMapper.Lib.Helper
{
    public class Settings
    {
        public string MidiIn { get; set; }
        public string MidiOut { get; set; }
        public int Max { get; set; }
        public double Exponent { get; set; }
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
