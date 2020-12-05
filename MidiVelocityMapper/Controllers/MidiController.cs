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
    public class MidiController : ControllerBase
    {
        private readonly SettingsHelper settingsHelper;
        public MidiController()
        {
            settingsHelper = new SettingsHelper(Constants.SettingsFile);
        }

        [HttpGet]
        public IList<VelocityMap> GetCurrentVelocityMap()
        {
            var map = MidiVelocityMapper.Instance.Calculator.GetMap();
            return map;
        }

        [HttpPost]
        public void SetCurve(string name)
        {
            var curve = settingsHelper.GetSettings().Curves.SingleOrDefault(x => x.Name == name);
            if (curve != null)
            {
                var calculator = new DefaultVelocityCalculator(curve.Map);
                MidiVelocityMapper.Instance.Calculator = calculator;
            }
        }

        [HttpPost]
        public void SetDefaultMap()
        {
            SetCurve("Default");
        }

        [HttpPost]
        public void SetCustomVelocityMap(IList<VelocityMap> map)
        {
            var calculator = new DefaultVelocityCalculator(map);
            MidiVelocityMapper.Instance.Calculator = calculator;
        }

        [HttpGet]
        public IList<string> GetMidiInputDevices()
        {
            return MidiAccessManager.Default.Inputs.Select(x => x.Name).ToList();
        }

        [HttpGet]
        public IList<string> GetMidiOutputDevices()
        {
            return MidiAccessManager.Default.Outputs.Select(x => x.Name).ToList();
        }

        [HttpGet]
        public string GetCurrentMidiInputDevice()
        {
            return MidiVelocityMapper.Instance.GetMidiInputDevice();
        }

        [HttpPost]
        public void SetMidiInputDevice(string device)
        {
            MidiVelocityMapper.Instance.SetMidiInputDevice(device);
        }

        [HttpGet]
        public string GetCurrentMidiOutputDevice()
        {
            return MidiVelocityMapper.Instance.GetMidiOutputDevice();
        }

        [HttpPost]
        public void SetMidiOutputDevice(string device)
        {
            MidiVelocityMapper.Instance.SetMidiOutputDevice(device);
        }

        [HttpPost]
        public IList<VelocityMap> ConvertPianoteqVelocityCurve(string value)
        {
            var pattern = @"Velocity\s*=\s*\[(?<in>\s*\d*,?\s*)*;(?<out>\s*\d*,?\s*)*";

            var match = Regex.Match(value, pattern);
            if (match.Success)
            {
                var result = new List<VelocityMap>();

                var @in = match.Groups["in"].Captures.Select(x => x.Value.Replace(",", "").Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert.ToInt32(x)).ToList();
                var @out = match.Groups["out"].Captures.Select(x => x.Value.Replace(",", "").Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert.ToInt32(x)).ToList();
                for (int i = 0; i < @in.Count; i++)
                {
                    result.Add(new VelocityMap(@in[i], @out[i]));
                }

                return result;
            }

            return new List<VelocityMap>();
        }
    }
}
