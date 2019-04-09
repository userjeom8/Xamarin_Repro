using CompucareWard.Models;
using DevExpress.Mobile.DataGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using CompucareWard.Controls.Enums;

namespace CompucareWard.Controls.AttachedProperties
{
    public static class SHProperties
    {
        public static readonly BindableProperty KeyboardButtonsProperty = BindableProperty.CreateAttached("KeyboardButtons", typeof(KeyboardButton), typeof(SHProperties), KeyboardButton.None);

        public static KeyboardButton GetKeyboardButtons(View view)
        {
            return (KeyboardButton)view.GetValue(KeyboardButtonsProperty);
        }

        public static void SetKeyboardButtons(View view, KeyboardButton value)
        {
            view.SetValue(KeyboardButtonsProperty, value);
        }

        public static readonly BindableProperty UserProperty = BindableProperty.CreateAttached("User", typeof(User), typeof(SHProperties), null);

        public static User GetUser(Page view)
        {
            return (User)view.GetValue(UserProperty);
        }

        public static void SetUser(Page view, User value)
        {
            view.SetValue(UserProperty, value);
        }

        public static readonly BindableProperty BadgeTextProperty = BindableProperty.CreateAttached("BadgeText", typeof(string), typeof(SHProperties), "0");
        public static string GetBadgeText(Element view)
        {            
            return (string)view.GetValue(BadgeTextProperty);
        }
        public static void SetBadgeText(Element view, string value)
        {
            view.SetValue(BadgeTextProperty, value);
        }

        public static readonly BindableProperty IconProperty = BindableProperty.CreateAttached("Icon", typeof(string), typeof(SHProperties), null);

        public static string GetIcon(Element view)
        {
            return (string)view.GetValue(IconProperty);
        }

        public static void SetIcon(Element view, string value)
        {
            view.SetValue(IconProperty, value);
        }

        public static readonly BindableProperty IconFontProperty = BindableProperty.CreateAttached("IconFont", typeof(string), typeof(SHProperties), "Font Awesome 5 Pro");

        public static string GetIconFont(ToolbarItem view)
        {
            return (string)view.GetValue(IconFontProperty);
        }

        public static void SetIconFont(ToolbarItem view, string value)
        {
            view.SetValue(IconFontProperty, value);
        }

        public static readonly BindableProperty IconFontSizeProperty = BindableProperty.CreateAttached("IconFontSize", typeof(int), typeof(SHProperties), 28);

        public static int GetIconFontSize(ToolbarItem view)
        {
            return (int)view.GetValue(IconFontSizeProperty);
        }

        public static void SetIconFontSize(ToolbarItem view, int value)
        {
            view.SetValue(IconFontSizeProperty, value);
        }

        public static readonly BindableProperty IsLeftToolbarItemProperty = BindableProperty.CreateAttached("IsLeftToolbarItem", typeof(bool), typeof(SHProperties), false);

        public static bool GetIsLeftToolbarItem(ToolbarItem view)
        {
            return (bool)view.GetValue(IsLeftToolbarItemProperty);
        }

        public static void SetIsLeftToolbarItem(ToolbarItem view, bool value)
        {
            view.SetValue(IsLeftToolbarItemProperty, value);
        }

        public static readonly BindableProperty GridItemsSourceProperty = BindableProperty.CreateAttached("GridItemsSource", typeof(IEnumerable), typeof(SHProperties), null, 
            propertyChanged: OnGridItemsSourceChanged);

        public static IEnumerable GetGridItemsSource(Element view)
        {
            return (IEnumerable)view.GetValue(GridItemsSourceProperty);
        }

        public static void SetGridItemsSource(Element view, IEnumerable value)
        {
            view.SetValue(GridItemsSourceProperty, value);
        }

        static void OnGridItemsSourceChanged<IEnumerable>(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
        {
            var grid = bindable as GridControl;
            var items = (newValue as IList<GroupedFormResult>)?.FirstOrDefault()?.Results;

            if (items != null)
            {
                int index = 0;

                foreach (var column in grid.Columns.Skip(1))
                {
                    var element = items.Count > index ? items[index] : null;
                    column.Caption = element?.DateTime.ToString("HH:mm") ?? "-";
                    index++;
                }
            }
        }
    }
}
