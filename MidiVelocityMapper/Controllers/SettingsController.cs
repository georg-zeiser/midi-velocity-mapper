using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Commons.Music.Midi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MidiVelocityMapper.Helper;
using MidiVelocityMapper.Models;
using MidiVelocityMapper.VelocityCalculators;

namespace MidiVelocityMapper.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SettingsController : ControllerBase
    {
        private readonly SettingsHelper settingsHelper;
        public SettingsController()
        {
            settingsHelper = new SettingsHelper(Constants.SettingsFile);
        }

        [HttpGet]
        public Settings GetSettings()
        {
            return settingsHelper.GetSettings();
        }

        [HttpPost]
        public void SaveSettings(Settings settings)
        {
            settingsHelper.SaveSettings(settings);
        }
    }
}
