using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace CompucareWard.Controls.AttachedProperties
{
    public static class BindableLayout
    {
        public static IEnumerable GetItemsSource(BindableObject bindableObject)
            => (IEnumerable)bindableObject.GetValue(ItemsSourceProperty);
        public static void SetItemsSource(BindableObject bindableObject, IEnumerable value)
            => bindableObject.SetValue(ItemsSourceProperty, value);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.CreateAttached("ItemsSource", typeof(IEnumerable), typeof(BindableLayout), null,
            propertyChanged: (bindable, oldValue, newValue) => OnItemsSourceChanged(bindable, (IEnumerable)newValue));

        public static DataTemplate GetItemTemplate(BindableObject bindableObject)
            => (DataTemplate)bindableObject.GetValue(ItemTemplateProperty);
        public static void SetItemTemplate(BindableObject bindableObject, DataTemplate value)
            => bindableObject.SetValue(ItemTemplateProperty, value);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.CreateAttached("ItemTemplate", typeof(DataTemplate), typeof(BindableLayout), null);

        public static readonly BindableProperty IsBusyProperty = BindableProperty.CreateAttached("IsBusy", typeof(bool), typeof(BindableLayout), defaultValue: false,
            propertyChanged: (b, o, n) => OnIsBusyChanged(b, (bool)n));
        public static string GetIsBusy(BindableObject bindableObject)
            => (string)bindableObject.GetValue(IsBusyProperty);
        public static void SetIsBusy(BindableObject bindableObject, string value)
            => bindableObject.SetValue(IsBusyProperty, value);

        private const string _busyIndicatorId = "BusyIndicatorId";

        async static void OnIsBusyChanged(BindableObject control, bool isBusy)
        {
            if (control is TableSection section)
            {
                if (isBusy)
                    section.Insert(0, new ViewCell { View = new ActivityIndicator() { IsRunning = true, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(10), StyleId = _busyIndicatorId } });
                else
                {
                    await Task.Delay(500);

                    if (section.FirstOrDefault(i => i.StyleId == _busyIndicatorId) is ViewCell busyIndicator)
                        section.Remove(busyIndicator);
                }
            }
        }

        static void OnItemsSourceChanged(BindableObject control, IEnumerable newDataItems)
        {
            if (control is TableSection section && GetItemTemplate(section) is DataTemplate template)
            {
                foreach (var item in section.Where(i => i.StyleId != _busyIndicatorId).ToList())
                    section.Remove(item);

                if (newDataItems != null)
                {
                    foreach (object dataItem in newDataItems)
                    {
                        var item = template.CreateContent() as ViewCell;
                        item.BindingContext = dataItem;
                        section.Add(item);
                    }
                }
            }
        }
    }
}
