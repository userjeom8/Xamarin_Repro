using CompucareWard.Controls;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllPatientsPage : SearchPage
    {
		public AllPatientsPage()
		{
			InitializeComponent();
		}

        void OnLocationButtonClick(object sender, EventArgs e)
        {
            LocationPicker.IsVisible = true;
            LocationPicker.IsEnabled = true;
            LocationPicker.Focus();
            LocationPicker.Unfocused += LocationPicker_Unfocused;
        }

        private void LocationPicker_Unfocused(object sender, FocusEventArgs e)
        {
            LocationPicker.Unfocused -= LocationPicker_Unfocused;
            LocationPicker.IsVisible = false;
            LocationPicker.IsEnabled = false;
        }
    }
}