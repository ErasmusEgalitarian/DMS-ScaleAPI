using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace DevGUIAPPv2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Button clicked!");
    }
}
