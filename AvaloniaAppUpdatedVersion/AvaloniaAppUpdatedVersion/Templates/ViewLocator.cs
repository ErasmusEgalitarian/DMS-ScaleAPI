using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaAppUpdatedVersion.ViewModels;
using System;

namespace AvaloniaAppUpdatedVersion.Templates
{
    // Ensure your ViewLocator implements the correct interface with the expected return type
    public class ViewLocator : IDataTemplate
    {
        public static bool SupportsRecycling => false;

        public Control Build(object? data)
        {

            if (data is null)
                return null;
            
            var viewName = data.GetType().FullName.Replace("ViewModel", "View", StringComparison.InvariantCulture);
            var type = Type.GetType(viewName);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + viewName };
            }
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}