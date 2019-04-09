using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompucareWard.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoundButton : ContentView
	{
		public RoundButton()
		{
			InitializeComponent();
		}

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(RoundButton), null);

        public string Icon
        {
            set { SetValue(IconProperty, value); }
            get { return (string)GetValue(IconProperty); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RoundButton), null);

        public ICommand Command
        {
            set { SetValue(CommandProperty, value); }
            get { return (ICommand)GetValue(CommandProperty); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RoundButton), null);

        public object CommandParameter
        {
            set { SetValue(CommandParameterProperty, value); }
            get { return (object)GetValue(CommandParameterProperty); }
        }
    }
}