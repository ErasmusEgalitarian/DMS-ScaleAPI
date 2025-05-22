using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaAppUpdatedVersion.Services;
using AvaloniaAppUpdatedVersion.Data;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class Scale1PageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;

        // Runtime constructor
        public Scale1PageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PageName = ApplicationPageNames.Scale1;
        }

        // Design-time constructor
        public Scale1PageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.ScaleOverview;
        }


        [RelayCommand]
        public void ToScaleOverview()
        {
            _navigationService.NavigateTo(ApplicationPageNames.ScaleOverview);
        }
    }
}
