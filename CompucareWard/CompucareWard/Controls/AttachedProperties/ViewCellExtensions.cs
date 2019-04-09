using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompucareWard.Controls.AttachedProperties
{
    public static class ViewCellExtensions
    {
        public static ICommand GetTappedCommand(BindableObject bindableObject) => (ICommand)bindableObject.GetValue(TappedCommandProperty);
        public static void SetTappedCommand(BindableObject bindableObject, object value) => bindableObject.SetValue(TappedCommandProperty, value);

        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.CreateAttached("TappedCommand", typeof(ICommand), typeof(ViewCellExtensions), default(ICommand), BindingMode.OneWay, null, PropertyChanged);

        public static object GetTappedCommandParameter(BindableObject bindableObject) => bindableObject.GetValue(TappedCommandParameterProperty);
        public static void SetTappedCommandParameter(BindableObject bindableObject, object value) => bindableObject.SetValue(TappedCommandParameterProperty, value);
        public static readonly BindableProperty TappedCommandParameterProperty =
            BindableProperty.CreateAttached("TappedCommandParameter", typeof(object), typeof(ViewCellExtensions), default(object), BindingMode.OneWay, null);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ViewCell cell)
            {
                cell.Tapped -= ViewCellOnTapped;
                cell.Tapped += ViewCellOnTapped;
            }
        }

        private static void ViewCellOnTapped(object sender, EventArgs e)
        {
            if (sender is ViewCell cell && cell.IsEnabled)
            {
                var parameter = GetTappedCommandParameter(cell);

                if (GetTappedCommand(cell) is ICommand command && command.CanExecute(parameter))
                    command.Execute(parameter);
            }
        }
    }
}
