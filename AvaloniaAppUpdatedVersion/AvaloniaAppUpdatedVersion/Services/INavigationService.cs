using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaAppUpdatedVersion.ViewModels;

namespace AvaloniaAppUpdatedVersion.Services
{
    public interface INavigationService
    {
        void NavigateTo(ViewModelBase target);
    }
}
