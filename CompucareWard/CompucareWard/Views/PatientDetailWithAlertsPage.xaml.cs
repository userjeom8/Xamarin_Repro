using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using CompucareWard.Models;
using CompucareWard.ViewModels;
using Prism.Ioc;
using Prism.Navigation;

namespace CompucareWard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientDetailWithAlertsPage : CarouselPage
    {
        public PatientDetailWithAlertsPage()
        {
            InitializeComponent();
        }
    }
}