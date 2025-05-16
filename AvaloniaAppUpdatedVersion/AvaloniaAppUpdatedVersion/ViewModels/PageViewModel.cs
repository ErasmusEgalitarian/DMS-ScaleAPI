using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaAppUpdatedVersion.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaAppUpdatedVersion.ViewModels
{
    public partial class PageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ApplicationPageNames _pageName;
    }
}
