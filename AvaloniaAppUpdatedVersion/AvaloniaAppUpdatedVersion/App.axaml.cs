using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using AvaloniaAppUpdatedVersion.ViewModels;
using AvaloniaAppUpdatedVersion.Views;
using AvaloniaAppUpdatedVersion.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

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
        collection.AddSingleton<Scale1PageViewModel>();

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
