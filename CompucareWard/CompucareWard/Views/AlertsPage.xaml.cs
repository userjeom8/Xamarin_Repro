using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlertsPage : PatientDetailChildPage
    {
		public AlertsPage() : base()
		{
			InitializeComponent();
        }
    }
}