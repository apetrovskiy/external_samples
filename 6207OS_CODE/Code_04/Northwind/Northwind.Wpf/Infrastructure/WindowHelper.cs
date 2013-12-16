using System;
using System.Windows;

namespace Northwind.Wpf.Infrastructure
{
    public static class WindowHelper
    {

        public static bool? GetDialogResult(DependencyObject obj)
        {
            return (bool?)obj.GetValue(DialogResultProperty);
        }

        public static void SetDialogResult(DependencyObject obj, bool? value)
        {
            obj.SetValue(DialogResultProperty, value);
        }

        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached("DialogResult", typeof(bool?),
            typeof(WindowHelper),
            new UIPropertyMetadata(null, OnPropertyChanged));

        public static bool GetIsClosed(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsClosedProperty);
        }

        public static void SetIsClosed(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClosedProperty, value);
        }

        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.RegisterAttached("IsClosed",
            typeof(bool),
            typeof(WindowHelper),
            new UIPropertyMetadata(false, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as IView;
            if (view == null)
            {
                throw new NotSupportedException("Only IView type is supported.");
            }

            if (e.Property == DialogResultProperty)
            {
                view.DialogResult = (bool?)e.NewValue;
            }
            else if (e.Property == IsClosedProperty)
            {
                view.Close();
            }
        }

    }
}
