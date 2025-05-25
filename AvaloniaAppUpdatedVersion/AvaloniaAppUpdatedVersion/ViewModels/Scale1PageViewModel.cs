using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaAppUpdatedVersion.Services;
using AvaloniaAppUpdatedVersion.Data;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using AvaloniaAppUpdatedVersion.ViewModels.Charts;
using System.Threading;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class Scale1PageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;

        public RealTimeChartViewModel RealTimeChart { get; }

        public string Version => RealTimeChart.Version;

        public string Status => RealTimeChart.Status;

        public string StatusColor => Status switch
        {
            "Up" => "DarkGreen",
            "Down" => "DarkRed",
            _ => "DarkRed"
        };


        // Runtime constructor
        public Scale1PageViewModel(INavigationService navigationService, RealTimeChartViewModel chart)
        {
            _navigationService = navigationService;
            PageName = ApplicationPageNames.Scale1;
            RealTimeChart = chart;

            // Hvis du vil opdatere UI automatisk ved ændringer:
            RealTimeChart.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(RealTimeChart.Status))
                {
                    OnPropertyChanged(nameof(Status));
                    OnPropertyChanged(nameof(StatusColor));
                }

                else if (e.PropertyName == nameof(RealTimeChart.Version))
                {
                    OnPropertyChanged(nameof(Version));
                
                }
            };
        }

        // Design-time constructor
        public Scale1PageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.Scale1;
        //    Version = "1.0.0"; // Example version
        //    Status = "Up"; // Example status
        //    StatusColor = "DarkGreen";
        }

        [RelayCommand]
        public void ToScaleOverview()
        {
            _navigationService.NavigateTo(ApplicationPageNames.ScaleOverview);
        }
    }
}
