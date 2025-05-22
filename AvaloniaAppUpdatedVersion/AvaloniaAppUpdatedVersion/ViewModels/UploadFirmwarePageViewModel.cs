using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaAppUpdatedVersion.Data;
using AvaloniaAppUpdatedVersion.Services;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class UploadFirmwarePageViewModel : PageViewModel
    {
        private readonly INavigationService? _navigationService;

        // Runtime constructor
        public UploadFirmwarePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PageName = ApplicationPageNames.UploadFirmware;
        }

        // Design-time constructor
        public UploadFirmwarePageViewModel()
        {
            if (!Design.IsDesignMode)
                throw new InvalidOperationException("This constructor is only for design-time.");

            PageName = ApplicationPageNames.UploadFirmware;
        }
    }
}
