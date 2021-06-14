using Commons.Music.Midi;
using MidiVelocityMapper.Lib.VelocityCalculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiVelocityMapper.Lib
{
    public class MidiVelocityMapper : IDisposable
    {
        #region singleton
        private MidiVelocityMapper() { }

        private static object _lock = new object();
        private static MidiVelocityMapper _instance;
        public static MidiVelocityMapper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MidiVelocityMapper();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        public delegate void VelocityConverted(int originalVelocity, int overriddenVelocity);
        public event VelocityConverted OnVelocityConverted;

        private object _inputLock = new object();
        private IMidiInput _input;
        private IMidiInput Input
        {
            get { return _input; }
            set
            {
                lock (_inputLock)
                {
                    if (_input?.Details.Id == value?.Details.Id)
                    {
                        return;
                    }

                    if (_input != null)
                    {
                        _input.CloseAsync();
                        _input = null;
                    }

                    if (value != null)
                    {
                        _input = value;
                        _input.MessageReceived += OnMidiMessageReceived;
                    }
                }
            }
        }

        private object _outputLock = new object();
        private IMidiOutput _output;
        private IMidiOutput Output
        {
            get { return _output; }
            set
            {
                lock (_outputLock)
                {
                    if (_output?.Details.Id == value?.Details.Id)
                    {
                        return;
                    }

                    if (_output != null)
                    {
                        _output.CloseAsync();
                        _output = null;
                    }

                    if (value != null)
                    {
                        _output = value;
                    }
                }
            }
        }

        public IVelocityCalculator Calculator { get; set; }

        public void Init(IVelocityCalculator calculator, string midiIn, string midiOut)
        {
            Calculator = calculator;

            SetCurrentMidiInputDevice(midiIn);
            SetCurrentMidiOutputDevice(midiOut);
        }

        public string GetCurrentMidiInputDevice()
        {
            return Input?.Details.Name;
        }

        public void SetCurrentMidiInputDevice(string midiIn)
        {
            if (GetCurrentMidiInputDevice() == midiIn)
            {
                return;
            }

            string deviceId = GetMidiInputDeviceId(midiIn);
            if (!string.IsNullOrWhiteSpace(deviceId))
            {
                Input = MidiAccessManager.Default.OpenInputAsync(deviceId).Result;
            }
            else
            {
                Input = null;
            }
        }

        public string GetCurrentMidiOutputDevice()
        {
            return Output?.Details.Name;
        }

        public void SetCurrentMidiOutputDevice(string midiOut)
        {
            if (GetCurrentMidiOutputDevice() == midiOut)
            {
                return;
            }

            string deviceId = GetMidiOutputDeviceId(midiOut);
            if (!string.IsNullOrWhiteSpace(deviceId))
            {
                Output = MidiAccessManager.Default.OpenOutputAsync(deviceId).Result;
            }
            else
            {
                Output = null;
            }
        }

        public IEnumerable<IMidiPortDetails> GetAllMidiInputDevices()
        {
            return MidiAccessManager.Default.Inputs;
        }

        public IEnumerable<IMidiPortDetails> GetAllMidiOutputDevices()
        {
            return MidiAccessManager.Default.Outputs;
        }

        private void OnMidiMessageReceived(object sender, MidiReceivedEventArgs e)
        {
            if (Output == null)
            {
                return;
            }

            if (Calculator != null && e.Data[0] == MidiEvent.NoteOn)
            {
                var note = e.Data[1];
                var velocity = e.Data[2];
                var newValue = Calculator.Calculate(velocity);

                Output.Send(new byte[] { MidiEvent.NoteOn, note, Convert.ToByte(newValue) }, e.Start, e.Length, e.Timestamp);
                OnVelocityConverted?.Invoke(velocity, newValue);
            }
            else
            {
                Output.Send(e.Data, e.Start, e.Length, e.Timestamp);
            }
        }

        #region helper methods
        private string GetMidiInputDeviceId(string deviceName)
        {
            return MidiAccessManager.Default.Inputs.Where(x => x.Name == deviceName).SingleOrDefault()?.Id;
        }

        private string GetMidiOutputDeviceId(string deviceName)
        {
            return MidiAccessManager.Default.Outputs.Where(x => x.Name == deviceName).SingleOrDefault()?.Id;
        }
        #endregion

        public void Dispose()
        {
            Input = null;
            Output = null;
        }
    }
}
