using System;
using System.Threading;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaloniaAppUpdatedVersion.Templates;
using AvaloniaAppUpdatedVersion.Services;
using AvaloniaAppUpdatedVersion.Data;
using AvaloniaAppUpdatedVersion.Factories;
using AvaloniaAppUpdatedVersion.Views;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using System.Diagnostics;

namespace AvaloniaAppUpdatedVersion.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly PageFactory _pageFactory;

    [ObservableProperty]
    private bool _IsPaneOpen = false;

    [ObservableProperty]
    private PageViewModel _currentPage;

    /// <summary>
    /// Design-time constructor for design-time data
    /// </summary>
    public MainViewModel()
    {
       CurrentPage = new ScaleOverviewPageViewModel();
    }


    public MainViewModel(INavigationService navigationService, PageFactory pageFactory)
    {
        _navigationService = navigationService;
        _pageFactory = pageFactory;

        _navigationService.OnPageChanged += pageName =>
        {
            CurrentPage = _pageFactory.GetPageViewModel(pageName);
        };

        // Start side
        NavigateToHome();

        // Initialize();
        //Debug.WriteLine("Tis");
        // Thread.Sleep(10000);



    }

    //private async void Initialize()
    //{
    // var service = new APIService();
    //string token = await service.Authenticate("wasteworker", "verysecretpassword");
    //}

    [RelayCommand]
    public void NavigateToHome() => _navigationService.NavigateTo(ApplicationPageNames.Home);

    [RelayCommand]
    public void NavigateToScale1() => _navigationService.NavigateTo(ApplicationPageNames.Scale1);

    [RelayCommand]
    public void NavigateToScaleOverview() => _navigationService.NavigateTo(ApplicationPageNames.ScaleOverview);

    [ObservableProperty]
    private ListItemTemplate? _selectedItem;  

partial void OnSelectedItemChanged(ListItemTemplate? value)
    {
        if (value != null)
        {
            // Map the selected item's label to the corresponding ApplicationPageNames enum value
            if (Enum.TryParse<ApplicationPageNames>(value.Label, out var pageName))
            {
                _navigationService.NavigateTo(pageName);
            }
            else
            {
                throw new ArgumentException($"No matching ApplicationPageNames value for label '{value.Label}'.");
            }
        }
    }

    public ObservableCollection<ListItemTemplate> Items { get; } =
    [
        new ListItemTemplate(typeof(HomePageViewModel), "HomeRegular"),
        new ListItemTemplate(typeof(UploadFirmwarePageViewModel), "ArrowUploadRegular"),
        new ListItemTemplate(typeof(ScaleOverviewPageViewModel), "NetworkCheckRegular"),
    ];

    [RelayCommand]
    public void TriggerPaneCommand()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");

        // Replace ApplicationException with Application to fix the error
        Application.Current!.TryFindResource(iconKey, out var res);
        if (res is StreamGeometry geometry)
        {
            ItemIcon = geometry;
        }
        else
        {
            throw new Exception($"Resource '{iconKey}' not found.");
        }
    }
    public string Label { get; }
    public Type ModelType { get; }
    public StreamGeometry ItemIcon { get; }
}

