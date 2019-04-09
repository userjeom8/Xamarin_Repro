using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace CompucareWard.Views
{
    public abstract class PatientDetailChildPage : ContentPage
    {
        public PatientDetailChildPage()
        {
            SelectedPageChangedCommand = new DelegateCommand<string>(SelectedPageChanged);
        }

        public static readonly BindableProperty SelectedPageChangedCommandProperty = BindableProperty.Create(nameof(SelectedPageChangedCommand), typeof(ICommand), typeof(PatientDetailChildPage), null);

        public ICommand SelectedPageChangedCommand
        {
            get => (ICommand)GetValue(SelectedPageChangedCommandProperty);
            set => SetValue(SelectedPageChangedCommandProperty, value);
        }

        void SelectedPageChanged(string title)
        {
            if (Parent is CarouselPage parentPage && (parentPage.SelectedItem as Page)?.Title != title)
                parentPage.SelectedItem = parentPage.Children.FirstOrDefault(p => p.Title == title);
        }
    }
}
