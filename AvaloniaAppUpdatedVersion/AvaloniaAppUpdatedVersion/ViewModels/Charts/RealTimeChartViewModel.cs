using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using AvaloniaAppUpdatedVersion.Services;
using AvaloniaAppUpdatedVersion.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Rendering;

namespace AvaloniaAppUpdatedVersion.ViewModels.Charts
{
    public partial class RealTimeChartViewModel : ViewModelBase
    {
        private readonly APIService _apiService;
        private readonly Random _random = new();
        private readonly List<DateTimePoint> _values = [];
        private readonly DateTimeAxis _customXAxis;
        private readonly Axis _customYAxis;
        private readonly string _seriesName = "Scale Up-status";

        [ObservableProperty]
        private string status;

        [ObservableProperty]
        private string version;

        private int StatusBinary;

        public ObservableCollection<ISeries> Series { get; set; }

        public Axis[] XAxes { get; set; }

        public Axis[] YAxes { get; set; }

        public string SeriesName => _seriesName;

        public object Sync { get; } = new object();

        public bool IsReading { get; set; } = true;

        public bool GettingStatus { get; set; } = true;

        public SolidColorPaint LegendTextPaint { get; set; }

        public RealTimeChartViewModel(APIService apiService)
        {
            _apiService = apiService;

            LegendTextPaint = new SolidColorPaint(SKColors.White);

            Series = [
                new LineSeries<DateTimePoint>
            {
                Values = _values,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                Name = _seriesName
            }
            ];

            _customXAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(100)),
                Name = "Time",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.White)
            };

            _customYAxis = new Axis
            {
                MinLimit = -0.2,
                MaxLimit = 1.2,
                MinStep = 1,
                Name = "Down / Up  =  0 / 1",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.White)
            };

            XAxes = [_customXAxis];
            YAxes = [_customYAxis];

            InitializeAPIAuth(); // Wait for authentication to complete before proceeding

            InitializeAsync(); 
        }

        private async Task InitializeAsync()
        {
            await Task.Delay(2000); // Wait for 2 seconds to ensure the API is ready
            string scaleID = "wasteworker";
            await ExecuteScaleStatusCheck(scaleID); // én gang først

            _ = RunScaleStatusCheckerLoop(); // så starter løkken

            _ = RunScaleVersionCheckerLoop();

            _ = ReadData(); // nu starter grafen
        }

        private async Task InitializeAPIAuth()
        {
            await _apiService.Authenticate("wasteworker", "verysecretpassword");
        }

        private async Task RunScaleVersionCheckerLoop()
        {

            string scaleID = "wasteworker";

            while (GettingStatus == true)
            {
                await ExecuteScaleVersionCheck(scaleID);
                await Task.Delay(10000); // 10 sekunders ventetid
            }
        }

        private async Task ExecuteScaleVersionCheck(string scaleID)
        {
            Version = await _apiService.GetScaleVersion(scaleID); // Use the instance of APIService
            Console.WriteLine($"Version at call {scaleID}: {Version}");


        }

        private async Task RunScaleStatusCheckerLoop()
        {

            string scaleID = "wasteworker";
            while (GettingStatus == true)
            {
                await ExecuteScaleStatusCheck(scaleID);
                await Task.Delay(5000); // 5 sekunders ventetid
            }
        }

        private async Task ExecuteScaleStatusCheck(string scaleID)
        {
            Status = await _apiService.GetScaleStatus(scaleID);
            Console.WriteLine($"Status modtaget: '{Status}'");

            StatusBinary = Status switch
            {
                "Up" => 1,
                "Down" => 0,
                _ => 0 // unknown status, default to 0
            };

            Console.WriteLine($"StatusBinary sat til: {StatusBinary}");

            // Set StatusBinary manually for testing
            //StatusBinary = 1;

        }

        private async Task ReadData()
        {
            while (IsReading)
            {
                await Task.Delay(5000);

                // Because we are updating the chart from a different thread 
                // we need to use a lock to access the chart data. 
                // this is not necessary if your changes are made on the UI thread. 
                lock (Sync)
                {
                    _values.Add(new DateTimePoint(DateTime.Now, StatusBinary));
                    if (_values.Count > 120) _values.RemoveAt(0);

                    // we need to update the separators every time we add a new point 
                    _customXAxis.CustomSeparators = GetSeparators();

                    // UI-thread opdatering
                    Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        Series[0].Values = _values.ToList();
                    });
                }
            }
        }

        private static double[] GetSeparators()
        {
            var now = DateTime.Now;

            return
            [
            now.AddSeconds(-600).Ticks,
            now.AddSeconds(-540).Ticks,
            now.AddSeconds(-480).Ticks,
            now.AddSeconds(-420).Ticks,
            now.AddSeconds(-360).Ticks,
            now.AddSeconds(-300).Ticks,
            now.AddSeconds(-240).Ticks,
            now.AddSeconds(-180).Ticks,
            now.AddSeconds(-120).Ticks,
            now.AddSeconds(-60).Ticks,
            now.Ticks
            ];
        }

        private static string Formatter(DateTime date)
        {
            var secsAgo = (DateTime.Now - date).TotalSeconds / 60;

            return secsAgo < 1
                ? "now"
                : $"{secsAgo:N0}m ago";
        }

        [ObservableProperty]
        private VersionLog? _newVersion;

        partial void OnVersionChanged(string oldValue, string newValue)
        {
            if (newValue != null && newValue != oldValue)
            {
                // Create a new VersionLogList instance with the updated version
                AddVersionToLog(newValue);
            }
        }

        public ObservableCollection<VersionLog> VersionLog { get; } = new();

        private void AddVersionToLog(string newVersion)
        {
            VersionLog.Add(new VersionLog(newVersion));
            // evt. begræns loglængde for ikke at vokse uendeligt
        }
    }

    public class VersionLog
    {
        public string Version { get; }
        public VersionLog(string version)
        {
            Version = version;

            // Could be expanded with:
            // Date = date;
            // Description = description;
        }
        
        public string VersionLabel => $"Version: {Version}"; // For display purposes
    }

}

