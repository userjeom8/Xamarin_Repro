using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckBox : ContentView
	{
		public CheckBox()
		{
			InitializeComponent();
		}

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), defaultValue: false, 
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckBox), defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e) => SetValue(IsCheckedProperty, !IsChecked);
    }
}