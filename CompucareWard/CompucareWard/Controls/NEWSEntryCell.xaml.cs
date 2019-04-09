using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NEWSEntryCell : NEWSViewCell<Entry>
    {
		public NEWSEntryCell() 
            : base()
        {
			InitializeComponent();

            ViewCellRootGrid.SetBinding(Grid.BindingContextProperty, new Binding(nameof(Component), mode: BindingMode.OneWay, source: this));
        }

        protected override Entry Editor => EntryInternal;

        private void OnEditorButtonClick(object sender, EventArgs e)
        {
            FocusControl(null);
        }        
    }
}