using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using AvaloniaAppUpdatedVersion.ViewModels;
using AvaloniaAppUpdatedVersion.Views;
using AvaloniaAppUpdatedVersion.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using AvaloniaAppUpdatedVersion.Factories;
using System;
using AvaloniaAppUpdatedVersion.Data;

namespace AvaloniaAppUpdatedVersion;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {

        var collection = new ServiceCollection();
        collection.AddSingleton<MainViewModel>();
        collection.AddTransient<Scale1PageViewModel>();
        collection.AddTransient<HomePageViewModel>();
        collection.AddTransient<StatusMonitorPageViewModel>();
        collection.AddTransient<UploadFirmwarePageViewModel>();
        collection.AddTransient<ScaleOverviewPageViewModel>();

        collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
        {
            ApplicationPageNames.Home => x.GetRequiredService<HomePageViewModel>(),
            ApplicationPageNames.Scale1 => x.GetRequiredService<Scale1PageViewModel>(),
            ApplicationPageNames.StatusMonitor => x.GetRequiredService<StatusMonitorPageViewModel>(),
            ApplicationPageNames.UploadFirmware => x.GetRequiredService<UploadFirmwarePageViewModel>(),
            ApplicationPageNames.ScaleOverview => x.GetRequiredService<ScaleOverviewPageViewModel>(),
            _ => throw new InvalidOperationException(),
        });

        collection.AddSingleton<PageFactory>();

        var services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {

            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {

            singleViewPlatform.MainView = new MainView
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
