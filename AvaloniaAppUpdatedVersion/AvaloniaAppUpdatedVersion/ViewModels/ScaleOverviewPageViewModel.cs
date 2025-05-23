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

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class ScaleOverviewPageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;
        private readonly APIService _APIService; // Add an instance of APIService

        [ObservableProperty]
        private string status;

        [ObservableProperty]
        private string statusColor;

        // Runtime constructor
        public ScaleOverviewPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _APIService = new APIService(); // Initialize the APIService instance
            PageName = ApplicationPageNames.ScaleOverview;

            _ = RunScaleStatusCheckerLoop(); // Kør loopet asynkront uden at blokere constructor
        }

        private async Task RunScaleStatusCheckerLoop()
        {
            string scaleID = "wasteworker";
            while (PageName == ApplicationPageNames.ScaleOverview)
            {
                await ExecuteScaleStatusCheck(scaleID);
                await Task.Delay(5000); // 5 sekunders ventetid
            }
        }

        private async Task ExecuteScaleStatusCheck(string scaleID)
        {
            Status = await _APIService.GetScaleStatus(scaleID); // Use the instance of APIService
            Console.WriteLine($"Status at call {scaleID}: {Status}");
        }

        partial void OnStatusChanged(string value)
        {
            StatusColor = value switch
            {
                "Up" => "DarkGreen",
                "Down" => "DarkRed",
                _ => "DarkRed"
            };
        }

        // Design-time constructor
        public ScaleOverviewPageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.ScaleOverview;

            StatusColor = "DarkGreen"; // Default color for design-time example
        }


        [RelayCommand]
        public void ToScale1()
        {
            _navigationService.NavigateTo(ApplicationPageNames.Scale1);
        }
    }
}
