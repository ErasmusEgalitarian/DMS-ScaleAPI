using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using AvaloniaAppUpdatedVersion.Templates;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class ScaleOverviewPageViewModel : ViewModelBase
    {
        private readonly MainViewModel _main;

        public ScaleOverviewPageViewModel()
        {
          
        }

        [RelayCommand]
        public void ToScale1()
        {
            _main.CurrentPage = new Scale1PageViewModel(); // Naviger evt. videre
        }
    }
}
