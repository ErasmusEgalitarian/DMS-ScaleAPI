using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaAppUpdatedVersion.Data;

namespace AvaloniaAppUpdatedVersion.Services
{
    public interface INavigationService
    {
        void NavigateTo(ApplicationPageNames pageName);
        ApplicationPageNames CurrentPage { get; }
        event Action<ApplicationPageNames>? OnPageChanged;
    }
}
