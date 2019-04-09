using CompucareWard.Helpers;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NEWSPickerEntryCell : NEWSViewCell<Entry>
    {
        public NEWSPickerEntryCell()
            : base()
        {
            InitializeComponent();

            PickerInternal.SetBinding(Picker.ItemsSourceProperty, new Binding(nameof(ItemsSource), mode: BindingMode.OneWay, source: this));
            PickerInternal.SetBinding(Picker.SelectedItemProperty, new Binding(nameof(SelectedItem), mode: BindingMode.TwoWay, source: this));

            MessagingCenter.Subscribe<Picker>(this, "Previous", (p) =>
            {
                if (p == PickerInternal && PreviousEntry != null)
                    PreviousEntry.FocusControl(FocusDirection.Back);
            });

            MessagingCenter.Subscribe<Picker>(this, "Next", (p) =>
            {
                if (p == PickerInternal && NextEntry != null)
                    FocusControl(FocusDirection.Forward);
            });
        }

        protected override Entry Editor => EntryInternal;

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(NEWSPickerEntryCell), null);

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(NEWSPickerEntryCell), null, defaultBindingMode: BindingMode.TwoWay);

        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private void OnEditorButtonClick(object sender, EventArgs e)
        {
            FocusControl(null);
        }

        private void PickerClicked(object sender, EventArgs e)
        {
            PickerInternal.Focus();
        }
    }
}