using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using CompucareWard.Views;

namespace CompucareWard.Controls
{
    public class TabItemPage : ContentPage
    {
        public TabItemPage()
        {
            SetBinding(IsSelectedProperty, new Binding(nameof(IsSelected)));
        }

        void SelectPage()
        {
            if (Parent is NavigationPage tab && tab.Parent is TabbedPage mainPage)
            {
                mainPage.CurrentPage = tab;
                SetValue(IsSelectedProperty, false);
            }
        }

        public static readonly BindableProperty BadgeNumberProperty = BindableProperty.Create(nameof(BadgeNumber), typeof(int), typeof(TabItemPage), 0);
        public int BadgeNumber
        {
            get => (int)GetValue(BadgeNumberProperty);
            set => SetValue(BadgeNumberProperty, value);      
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(TabItemPage), false, defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (b, o, n) => OnIsSelectedChanged(b, (bool)n));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        static void OnIsSelectedChanged(BindableObject b, bool isSelected)
        {
            if (isSelected && b is TabItemPage tabbedPage)
                tabbedPage.SelectPage();
        }
    }
}
