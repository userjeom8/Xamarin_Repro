using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Controls
{
    [ContentProperty("Items")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerCell : ViewCell
    {
        public PickerCell()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string), typeof(PickerCell), default(string));

        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(PickerCell), default(string));

        public static readonly BindableProperty SelectedValueProperty = BindableProperty.Create("SelectedValue", typeof(string), typeof(PickerCell), null, BindingMode.TwoWay,
                propertyChanged: (sender, oldValue, newValue) =>
                {
                    PickerCell pickerCell = (PickerCell)sender;

                    if (String.IsNullOrEmpty((string)newValue))
                    {
                        pickerCell.InternalPicker.SelectedIndex = -1;
                    }
                    else
                    {
                        pickerCell.InternalPicker.SelectedIndex =
                                pickerCell.Items.IndexOf((string)newValue);
                    }
                });

        public string Label
        {
            set { SetValue(LabelProperty, value); }
            get { return (string)GetValue(LabelProperty); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public IList<string> Items
        {
            get => InternalPicker.Items;
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs args)
        {
            if (InternalPicker.SelectedIndex == -1)
                SelectedValue = null;
            else
                SelectedValue = Items[InternalPicker.SelectedIndex];
        }
    }
}