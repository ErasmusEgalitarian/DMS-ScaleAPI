using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaAppUpdatedVersion.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _IsPaneOpen = true;

    [ObservableProperty]
    public ViewModelBase _CurrentPage = new HomePageViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedItem;

    partial void OnSelectedItemChanged(ListItemTemplate? value)
    {
        if (value != null)
        {
            CurrentPage = (ViewModelBase)Activator.CreateInstance(value.ModelType)!;
        }
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "HomeRegular"),
        new ListItemTemplate(typeof(StatusMonitorPageViewModel), "NetworkCheckRegular"),
        new ListItemTemplate(typeof(UploadFirmwarePageViewModel), "ArrowUploadRegular"),
        new ListItemTemplate(typeof(GridPageViewModel), "GridRegular"),
    };

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
