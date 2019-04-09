using CompucareWard.Controls;
using Foundation;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchPage), typeof(CompucareWard.iOS.Renderers.SearchPageRenderer))]
namespace CompucareWard.iOS.Renderers
{
    public class SearchPageRenderer : PageRenderer
    {
        UISearchController searchController;

        public override void WillMoveToParentViewController(UIViewController parent)
        {
            base.WillMoveToParentViewController(parent);            

            searchController = new UISearchController(searchResultsController: null)
            {
                HidesNavigationBarDuringPresentation = true,
                DimsBackgroundDuringPresentation = false,
                //ObscuresBackgroundDuringPresentation = true
            };            
            parent.NavigationItem.SearchController = searchController;            

            var element = (SearchPage)this.Element;

            searchController.SearchBar.CancelButtonClicked += (s, e) =>
            {
                element?.SearchCommand?.Execute(null);
            };

            searchController.SearchBar.TextChanged += (s, e) =>
            {
                element?.SearchCommand?.Execute(e.SearchText);
            };
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            using (var searchKey = new NSString("searchField"))
            {
                var textField = (UITextField)searchController.SearchBar.ValueForKey(searchKey);

                UIView backgroundView = textField.Subviews.FirstOrDefault();
                if (backgroundView != null)
                {
                    searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
                    backgroundView.BackgroundColor = UIColor.White;                    
                    backgroundView.Layer.CornerRadius = 10;
                    backgroundView.ClipsToBounds = true;
                }
            }
        }
    }
}
