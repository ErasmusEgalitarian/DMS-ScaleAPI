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

        // Runtime constructor
        public ScaleOverviewPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PageName = ApplicationPageNames.ScaleOverview;
        }

        // Design-time constructor
        public ScaleOverviewPageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.ScaleOverview;
        }


        [RelayCommand]
        public void ToScale1()
        {
            _navigationService.NavigateTo(ApplicationPageNames.Scale1);
        }
    }
}
