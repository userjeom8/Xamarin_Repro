using CompucareWard.UWP.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ToolbarItem), typeof(NavigationPageIconRenderer))]
namespace CompucareWard.UWP.Renderers
{
    public class NavigationPageIconRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            var itemsInfo = (this.Element as ContentPage).ToolbarItems;
            
            if (this.Element == null) return;

                    
        }        
    }
}
