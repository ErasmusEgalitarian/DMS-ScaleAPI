using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaAppUpdatedVersion.Data;
using AvaloniaAppUpdatedVersion.Factories;

namespace AvaloniaAppUpdatedVersion.Services
{
    public class NavigationService : INavigationService
    {
        private readonly PageFactory _pageFactory;

        public ApplicationPageNames CurrentPage { get; private set; }

        public event Action<ApplicationPageNames>? OnPageChanged;

        public NavigationService(PageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        public void NavigateTo(ApplicationPageNames pageName)
        {
            if (CurrentPage == pageName)
                return;

            CurrentPage = pageName;
            OnPageChanged?.Invoke(pageName);
        }
    }
}
