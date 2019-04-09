using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CompucareWard.Models;

namespace CompucareWard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NEWSAddPage : ContentPage
    {
        public NEWSAddPage()
        {
            InitializeComponent();
        }

        private void OxygenSupplementEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContentPage.IsVisible))
            {
                //UpdateChildrenLayout();
                //ForceLayout();
                //ParentViewCell.ForceUpdateSize();           
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OxygenSupplementEntry.PropertyChanged += OxygenSupplementEntry_PropertyChanged;
            TargetSaturationEntry.PropertyChanged += OxygenSupplementEntry_PropertyChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            OxygenSupplementEntry.PropertyChanged -= OxygenSupplementEntry_PropertyChanged;
            TargetSaturationEntry.PropertyChanged -= OxygenSupplementEntry_PropertyChanged;
        }
    }
}