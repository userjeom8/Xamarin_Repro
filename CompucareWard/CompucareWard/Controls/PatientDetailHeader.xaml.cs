using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientDetailHeader : ContentView
	{
		public PatientDetailHeader ()
		{
			InitializeComponent();
		}

        public static readonly BindableProperty TabSelectedCommandProperty = BindableProperty.Create(nameof(TabSelectedCommand), typeof(ICommand), typeof(PatientDetailHeader), null);

        public ICommand TabSelectedCommand
        {
            get => (ICommand)GetValue(TabSelectedCommandProperty);
            set => SetValue(TabSelectedCommandProperty, value);
        }

        public static readonly BindableProperty SelectedTitleProperty = BindableProperty.Create(nameof(SelectedTitle), typeof(string), typeof(PatientDetailHeader), null);

        public string SelectedTitle
        {
            get => (string)GetValue(SelectedTitleProperty);
            set => SetValue(SelectedTitleProperty, value);
        }

        public static readonly BindableProperty ShowAlertsProperty = BindableProperty.Create(nameof(ShowAlerts), typeof(bool?), typeof(PatientDetailHeader), false);

        public bool? ShowAlerts
        {
            get => (bool?)GetValue(ShowAlertsProperty);
            set => SetValue(ShowAlertsProperty, value);
        }
    }
}