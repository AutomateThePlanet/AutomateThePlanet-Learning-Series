using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Fidely.Demo.Windows.Views
{
    public static class DependencyObjectExtension
    {
        public static IEnumerable<T> FindDescendents<T>(this DependencyObject instance)
            where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(instance);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(instance, i);
                if (child is T)
                {
                    yield return child as T;
                }
                foreach (var descedent in child.FindDescendents<T>())
                {
                    yield return descedent as T;
                }
            }
        }
    }
}
