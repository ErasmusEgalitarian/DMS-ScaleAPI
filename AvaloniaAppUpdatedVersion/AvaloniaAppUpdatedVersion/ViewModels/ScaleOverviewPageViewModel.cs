using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using AvaloniaAppUpdatedVersion.Templates;
using AvaloniaAppUpdatedVersion.Services;
using AvaloniaAppUpdatedVersion.Data;
using AvaloniaAppUpdatedVersion.Factories;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls;
using AvaloniaAppUpdatedVersion.ViewModels.Charts;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class ScaleOverviewPageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;

        public RealTimeChartViewModel RealTimeChart { get; }

        public string Status => RealTimeChart.Status;

        public string StatusColor => Status switch
        {
            "Up" => "DarkGreen",
            "Down" => "DarkRed",
            _ => "DarkRed"
        };

        // Runtime constructor
        public ScaleOverviewPageViewModel(INavigationService navigationService, RealTimeChartViewModel chart)
        {
            _navigationService = navigationService;
            PageName = ApplicationPageNames.ScaleOverview;
            RealTimeChart = chart;

            RealTimeChart.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(RealTimeChart.Status))
                {
                    OnPropertyChanged(nameof(Status));
                    OnPropertyChanged(nameof(StatusColor));
                }
            };
        }

        // Design-time constructor
        public ScaleOverviewPageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.ScaleOverview;

           // StatusColor = "DarkGreen"; // Default color for design-time example
        }


        [RelayCommand]
        public void ToScale1()
        {
            _navigationService.NavigateTo(ApplicationPageNames.Scale1);
        }
    }
}
