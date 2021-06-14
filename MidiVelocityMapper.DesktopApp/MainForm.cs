using Commons.Music.Midi;
using MidiVelocityMapper.Lib.Helper;
using MidiVelocityMapper.Lib.VelocityCalculators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidiVelocityMapper.DesktopApp
{
    public partial class MainForm : Form
    {
        private SettingsHelper SettingsHelper = new SettingsHelper(Constants.SettingsFile);

        public MainForm()
        {
            InitializeComponent();
            SetupMidiVelopcityMapper();
            BindEvents();
            BindDebugEvents();
        }

        private void SetupMidiVelopcityMapper()
        {
            var settings = SettingsHelper.GetSettings();

            var midiIn = settings.MidiIn;
            var midiOut = settings.MidiOut;
            var max = settings.Max;
            var exponent = settings.Exponent;

            var calculator = new ExponentialVelocityCalculator(max, exponent);
            Lib.MidiVelocityMapper.Instance.Init(calculator, midiIn, midiOut);

            var input = Lib.MidiVelocityMapper.Instance.GetAllMidiInputDevices().Where(x => x.Name != midiOut).ToList();
            this.input.DataSource = input;
            this.input.SelectedItem = input.Where(x => x.Name == midiIn).SingleOrDefault();

            var output = Lib.MidiVelocityMapper.Instance.GetAllMidiOutputDevices().Where(x => x.Name != midiIn).ToList();
            this.output.DataSource = output;
            this.output.SelectedItem = output.Where(x => x.Name == midiOut).SingleOrDefault();

            this.max.Value = max;
            this.exponent.Value = Convert.ToDecimal(exponent);
        }

        private void BindEvents()
        {
            this.input.SelectedIndexChanged += OnDataChanged;
            this.output.SelectedIndexChanged += OnDataChanged;
            this.max.ValueChanged += OnDataChanged;
            this.exponent.ValueChanged += OnDataChanged;
        }

        private void UnBindEvents()
        {
            this.input.SelectedIndexChanged -= OnDataChanged;
            this.output.SelectedIndexChanged -= OnDataChanged;
            this.max.ValueChanged -= OnDataChanged;
            this.exponent.ValueChanged -= OnDataChanged;
        }

        private void OnDataChanged(object sender, EventArgs e)
        {
            UnBindEvents();

            var settings = new Settings()
            {
                MidiIn = this.input.Text,
                MidiOut = this.output.Text,
                Max = Convert.ToInt32(this.max.Value),
                Exponent = Convert.ToDouble(this.exponent.Value)
            };

            SettingsHelper.SaveSettings(settings);
            SetupMidiVelopcityMapper();

            BindEvents();
        }

        private void OnDrawComboboxItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var label = (comboBox.Items[e.Index] as IMidiPortDetails).Name;

            if (IsItemDisabled(comboBox, e.Index))
            {
                // NOTE we must draw the background or else each time we hover over the text it will be redrawn and its color will get darker and darker.
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                e.Graphics.DrawString(label, comboBox.Font, SystemBrushes.GrayText, e.Bounds);
            }
            else
            {
                e.DrawBackground();

                // Using winwaed's advice for selected items:
                // Set the brush according to whether the item is selected or not
                Brush brush = ((e.State & DrawItemState.Selected) > 0) ? SystemBrushes.HighlightText : SystemBrushes.ControlText;
                e.Graphics.DrawString(label, comboBox.Font, brush, e.Bounds);

                e.DrawFocusRectangle();
            }
        }

        private bool IsItemDisabled(ComboBox comboBox, int index)
        {
            ComboBox toCheck = this.input;
            if (comboBox == this.input)
            {
                toCheck = this.output;
            }

            return (comboBox.Items[index] as IMidiPortDetails).Name == toCheck.Text;
        }

        private void BindDebugEvents()
        {
            Lib.MidiVelocityMapper.Instance.OnVelocityConverted += (original, overridden) =>
            {
                Invoke(new Action(() =>
                {
                    this.debug.Text = $"{original} >> {overridden}";
                }));
            };
        }
    }
}
