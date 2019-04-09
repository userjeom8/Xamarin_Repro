using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CompucareWard.Models;
using CompucareWard.ViewModels;
using System.Windows.Input;
using Prism.Commands;

namespace CompucareWard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientOverviewPage : PatientDetailChildPage
    {
        public PatientOverviewPage() : base()
        {
            InitializeComponent();
        }
    }
}