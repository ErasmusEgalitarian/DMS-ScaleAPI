using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaAppUpdatedVersion.ViewModels;

namespace AvaloniaAppUpdatedVersion.Services
{
    public class NavigationService : INavigationService
    {
        private MainViewModel? _main;

        public void Init(MainViewModel main)
        {
            _main = main;
        }

        public void NavigateTo(ViewModelBase target)
        {
            if (_main is null)
                throw new InvalidOperationException("NavigationService is not initialized.");

            _main.CurrentPage = target;
        }
    }
}
