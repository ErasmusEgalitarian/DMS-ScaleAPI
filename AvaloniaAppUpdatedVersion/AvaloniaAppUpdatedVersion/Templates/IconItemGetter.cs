using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaAppUpdatedVersion.Templates
{
    public class IconItemGetter
    {
        public IconItemGetter(Type type, string iconKey, string label = "")
        {
            ModelType = type;
            Label = label;

            // Replace ApplicationException with Application to fix the error
            Application.Current!.TryFindResource(iconKey, out var res);
            if (res is StreamGeometry geometry)
            {
                Icon = geometry;
            }
            else
            {
                throw new Exception($"Resource '{iconKey}' not found.");
            }
        }
        public Type ModelType { get; }
        public StreamGeometry Icon { get; }
        public string Label { get; }
    }
}
