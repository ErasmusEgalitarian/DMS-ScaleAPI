using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaAppUpdatedVersion.Data;
using AvaloniaAppUpdatedVersion.Services;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class HomePageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;

        // Runtime constructor
        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PageName = ApplicationPageNames.Home;
        }

        // Design-time constructor
        public HomePageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.Home;
        }

        [RelayCommand]
        public void ToScaleOverview()
        {
            _navigationService.NavigateTo(ApplicationPageNames.ScaleOverview);
        }

        [RelayCommand]
        public void ToUploadFirmware()
        {
            _navigationService.NavigateTo(ApplicationPageNames.UploadFirmware);
        }
    }
}
