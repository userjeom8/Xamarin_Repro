//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("CompucareWard.Views.MainPage.xaml", "Views/MainPage.xaml", typeof(global::CompucareWard.Views.MainPage))]

namespace CompucareWard.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\MainPage.xaml")]
    public partial class MainPage : global::Xamarin.Forms.TabbedPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::CompucareWard.Views.MyPatientsPage MyPatients;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::CompucareWard.Views.AllPatientsPage AllPatients;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::CompucareWard.Views.RemindersPage Reminders;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(MainPage));
            MyPatients = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CompucareWard.Views.MyPatientsPage>(this, "MyPatients");
            AllPatients = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CompucareWard.Views.AllPatientsPage>(this, "AllPatients");
            Reminders = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CompucareWard.Views.RemindersPage>(this, "Reminders");
        }
    }
}