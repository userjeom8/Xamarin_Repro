using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompucareWard.Controls
{
    public class SearchPage : ContentPage
    {
        public string SearchString
        {
            get { return (string)GetValue(SearchStringProperty); }
            set { SetValue(SearchStringProperty, value); }
        }

        public static readonly BindableProperty SearchStringProperty = BindableProperty.Create("SearchString", typeof(string), typeof(SearchPage), null);

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create("SearchCommand", typeof(ICommand), typeof(SearchPage), null);
    }
}
