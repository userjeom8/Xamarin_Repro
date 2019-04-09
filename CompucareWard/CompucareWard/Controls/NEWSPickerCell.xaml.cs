using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NEWSPickerCell : NEWSViewCell<Picker>
    {
		public NEWSPickerCell() : base()
		{
			InitializeComponent();

            ViewCellRootGrid.SetBinding(Grid.BindingContextProperty, new Binding(nameof(Component), mode: BindingMode.OneWay, source: this));

            MessagingCenter.Subscribe<Picker>(this, "Clear", (p) =>
            {
                if (p == Editor)
                    Editor.SelectedItem = null;
            });
        }

        protected override Picker Editor => PickerInternal;

        private void OnEditorButtonClick(object sender, EventArgs e)
        {
            FocusControl(null);
        }
    }
}