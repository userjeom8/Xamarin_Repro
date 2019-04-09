using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CompucareWard.Models;
using CompucareWard.ViewModels;
using DevExpress.Mobile.DataGrid.Theme;
using DevExpress.Mobile.DataGrid;
using System.Windows.Input;
using Prism.Commands;

namespace CompucareWard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientObservationsPage : PatientDetailChildPage
    {
        public PatientObservationsPage() : base()
        {
            InitializeComponent();

            ThemeManager.ThemeName = Themes.Light;

            var headerFont = new ThemeFontAttributes(Font.Default.FontFamily, Device.GetNamedSize(NamedSize.Micro, typeof(Label)), FontAttributes.None, Color.Gray);
            ThemeManager.Theme.HeaderCustomizer.Font = headerFont;
            ThemeManager.Theme.HeaderCustomizer.Padding = 0;
            ThemeManager.Theme.CellCustomizer.SelectionColor = Color.White;
            ThemeManager.Theme.CellCustomizer.SelectionFontColor = Color.Black;
            var cellFont = new ThemeFontAttributes(Font.Default.FontFamily, Device.GetNamedSize(NamedSize.Small, typeof(Label)), FontAttributes.None, Color.Black);
            ThemeManager.Theme.CellCustomizer.Padding = 0;
            ThemeManager.Theme.CellCustomizer.Font = cellFont;
            ThemeManager.RefreshTheme();
        }
    }
}