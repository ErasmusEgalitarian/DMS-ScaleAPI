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
        private readonly MockAPIService _mockAPIService; // Add an instance of MockAPIService

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
            _mockAPIService = new MockAPIService(); // Initialize the MockAPIService instance
            PageName = ApplicationPageNames.Scale1;

            _ = RunScaleVersionCheckerLoop(); // Kør loopet asynkront uden at blokere constructor
            _ = RunScaleStatusCheckerLoop(); // Kør loopet asynkront uden at blokere constructor
        }

        private async Task RunScaleVersionCheckerLoop()
        {
            int callCount = 0;

            while (PageName == ApplicationPageNames.Scale1)
            {
                await ExecuteScaleVersionCheck(callCount);
                callCount++; // Øg tælleren
                await Task.Delay(10000); // 10 sekunders ventetid
            }
        }

        private async Task ExecuteScaleVersionCheck(int callCount)
        {
            Version = await _mockAPIService.GetScaleVersion(callCount); // Use the instance of MockAPIService
            Console.WriteLine($"Version at call {callCount}: {Version}");

            // Hvis du vil opdatere en ViewModel property:
            // CurrentVersion = version;
        }

        private async Task RunScaleStatusCheckerLoop()
        {
            int callCount = 0;
            while (PageName == ApplicationPageNames.Scale1)
            {
                await ExecuteScaleStatusCheck(callCount);
                callCount++; // Øg tælleren
                await Task.Delay(2000); // 2 sekunders ventetid
            }
        }

        private async Task ExecuteScaleStatusCheck(int callCount)
        {
            Status = await _mockAPIService.GetScaleStatus(callCount); // Use the instance of MockAPIService
            Console.WriteLine($"Status at call {callCount}: {Status}");
            // Hvis du vil opdatere en ViewModel property:
            // CurrentStatus = status;
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
