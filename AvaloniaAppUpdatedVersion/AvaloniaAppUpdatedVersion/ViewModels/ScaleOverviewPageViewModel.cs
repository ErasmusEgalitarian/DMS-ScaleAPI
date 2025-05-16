using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using AvaloniaAppUpdatedVersion.Templates;
using AvaloniaAppUpdatedVersion.Services;
using AvaloniaAppUpdatedVersion.Data;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class ScaleOverviewPageViewModel : PageViewModel
    {
        public ScaleOverviewPageViewModel()
        {
            PageName = ApplicationPageNames.ScaleOverview;
        }
        //[RelayCommand]
        //public void ToScale1()
        //{
            // Logic to navigate to Scale 1 page
            // For example, you might want to set the current page to Scale1PageViewModel
            // CurrentPage = new Scale1PageViewModel();
        //}
    }
}
