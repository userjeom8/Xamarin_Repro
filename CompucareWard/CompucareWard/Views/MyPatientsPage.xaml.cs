using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CompucareWard.Models;
using CompucareWard.Views;
using CompucareWard.ViewModels;
using CompucareWard.Controls;

namespace CompucareWard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyPatientsPage : TabItemPage
    {
        public MyPatientsPage()
        {
            InitializeComponent();
        }
    }
}