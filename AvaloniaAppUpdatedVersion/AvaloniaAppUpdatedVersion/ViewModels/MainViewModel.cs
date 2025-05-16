using System;
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

namespace AvaloniaAppUpdatedVersion.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private PageFactory _pageFactory;

    [ObservableProperty]
    private bool _IsPaneOpen = true;

    [ObservableProperty]
    private PageViewModel _currentPage;

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;

        ToHome();
    }

    [RelayCommand]
    private void ToScale1() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Scale1);

    [RelayCommand]
    private void ToHome() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Home);

    [ObservableProperty]
    private ListItemTemplate? _selectedItem;  

partial void OnSelectedItemChanged(ListItemTemplate? value)
    {
        if (value != null)
        {
            // Map the selected item's label to the corresponding ApplicationPageNames enum value
            if (Enum.TryParse<ApplicationPageNames>(value.Label, out var pageName))
            {
                CurrentPage = _pageFactory.GetPageViewModel(pageName);
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
        new ListItemTemplate(typeof(StatusMonitorPageViewModel), "NetworkCheckRegular"),
        new ListItemTemplate(typeof(UploadFirmwarePageViewModel), "ArrowUploadRegular"),
        new ListItemTemplate(typeof(ScaleOverviewPageViewModel), "GridRegular"),
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

