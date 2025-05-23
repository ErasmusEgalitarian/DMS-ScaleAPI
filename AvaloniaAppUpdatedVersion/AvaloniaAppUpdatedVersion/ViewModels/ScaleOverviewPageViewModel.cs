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
        private readonly MockAPIService _mockAPIService; // Add an instance of MockAPIService

        [ObservableProperty]
        private string status;

        [ObservableProperty]
        private string statusColor;

        // Runtime constructor
        public ScaleOverviewPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _mockAPIService = new MockAPIService(); // Initialize the MockAPIService instance
            PageName = ApplicationPageNames.ScaleOverview;

            _ = RunScaleStatusCheckerLoop(); // Kør loopet asynkront uden at blokere constructor
        }

        private async Task RunScaleStatusCheckerLoop()
        {
            int callCount = 0;
            while (PageName == ApplicationPageNames.ScaleOverview)
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
