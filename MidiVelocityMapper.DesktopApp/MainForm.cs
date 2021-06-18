using Commons.Music.Midi;
using MidiVelocityMapper.Lib.Helper;
using MidiVelocityMapper.Lib.VelocityCalculators;
using ScottPlot;
using ScottPlot.Plottable;
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
        private FormsPlot Chart;
        private ScatterPlot Plot;
        private ScatterPlot HighlightedPoint;

        private SettingsHelper SettingsHelper = new SettingsHelper(Constants.SettingsFile);

        public MainForm()
        {
            InitializeComponent();

            Chart = new FormsPlot();
            Chart.Size = this.pictureBox1.Size;
            Chart.Location = this.pictureBox1.Location;
            Chart.Plot.SetAxisLimits(0, 127, 0, 127);
            Chart.Plot.SetViewLimits(0, 127, 0, 127);
            Chart.Configuration.DoubleClickBenchmark = false;
            Chart.Configuration.LeftClickDragPan = false;
            Chart.Configuration.RightClickDragZoom = false;
            Chart.Configuration.ScrollWheelZoom = false;

            Chart.MouseMove += Chart_MouseMove;
            Chart.MouseLeave += Chart_MouseLeave;

            this.Controls.Add(Chart);
            this.Controls.Remove(this.pictureBox1);

            SetupMidiVelopcityMapper();
            UpdateChart();
            BindEvents();
            BindDebugEvents();
        }

        private void Chart_MouseLeave(object sender, EventArgs e)
        {
            Chart.Plot.Clear(typeof(Crosshair));
        }

        private void Chart_MouseMove(object sender, EventArgs e)
        {
            var point = Plot.GetPointNearestX(Chart.GetMouseCoordinates().x);

            Chart.Plot.Clear(typeof(Crosshair));
            Chart.Plot.AddCrosshair(point.x, point.y);
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

            var settings = new Lib.Helper.Settings()
            {
                MidiIn = this.input.Text,
                MidiOut = this.output.Text,
                Max = Convert.ToInt32(this.max.Value),
                Exponent = Convert.ToDouble(this.exponent.Value)
            };

            SettingsHelper.SaveSettings(settings);
            SetupMidiVelopcityMapper();
            UpdateChart();

            BindEvents();
        }

        private void BindDebugEvents()
        {
            Lib.MidiVelocityMapper.Instance.OnVelocityConverted += (original, overridden) =>
            {
                Invoke(new Action(() =>
                {
                    HighlightedPoint.Xs[0] = original;
                    HighlightedPoint.Ys[0] = overridden;
                    HighlightedPoint.IsVisible = true;
                    Chart.Render();
                }));
            };
        }

        private void UpdateChart()
        {
            var map = Lib.MidiVelocityMapper.Instance.Calculator.GetMap();
            double[] x = map.Select(x => Convert.ToDouble(x.In)).ToArray();
            double[] y = map.Select(x => Convert.ToDouble(x.Out)).ToArray();

            Chart.Plot.Clear();
            Plot = Chart.Plot.AddScatter(x, y, null, 1, 5, MarkerShape.none, LineStyle.Solid);
            Chart.Plot.AddScatter(x, x, Color.LightGray, 1, 5, MarkerShape.none, LineStyle.Dash);
            HighlightedPoint = Chart.Plot.AddPoint(0, 0);
            HighlightedPoint.Color = Color.Red;
            HighlightedPoint.MarkerSize = 10;
            HighlightedPoint.MarkerShape = MarkerShape.filledCircle;
            HighlightedPoint.IsVisible = false;
        }
    }
}
