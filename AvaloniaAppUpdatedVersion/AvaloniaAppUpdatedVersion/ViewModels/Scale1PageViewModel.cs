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

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class Scale1PageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;
        private readonly APIService _APIService; // Add an instance of APIService

        [ObservableProperty]
        private string version;

        [ObservableProperty]
        private string status;

        [ObservableProperty]
        private string statusColor;


        // Runtime constructor
        public Scale1PageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _APIService = new APIService(); // Initialize the APIService instance
            PageName = ApplicationPageNames.Scale1;

            _ = RunScaleVersionCheckerLoop(); // Kør loopet asynkront uden at blokere constructor
            _ = RunScaleStatusCheckerLoop(); // Kør loopet asynkront uden at blokere constructor
        }

        private async Task RunScaleVersionCheckerLoop()
        {
            string scaleID = "wasteworker";

            while (PageName == ApplicationPageNames.Scale1)
            {
                await ExecuteScaleVersionCheck(scaleID);
                //callCount++; // Øg tælleren
                await Task.Delay(10000); // 10 sekunders ventetid
            }
        }

        private async Task ExecuteScaleVersionCheck(string scaleID)
        {
            Version = await _APIService.GetScaleVersion(scaleID); // Use the instance of APIService
            Console.WriteLine($"Version at call {scaleID}: {Version}");

    
        }

        private async Task RunScaleStatusCheckerLoop()
        {
            string scaleID = "wasteworker";
            while (PageName == ApplicationPageNames.Scale1)
            {
                await ExecuteScaleStatusCheck(scaleID);
                //callCount++; // Øg tælleren
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
        public Scale1PageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.ScaleOverview;
            Version = "1.0.0"; // Example version
            Status = "Up"; // Example status
            StatusColor = "DarkGreen";
        }

        [RelayCommand]
        public void ToScaleOverview()
        {
            _navigationService.NavigateTo(ApplicationPageNames.ScaleOverview);
        }
    }
}
