using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using CompucareWard.Controls.AttachedProperties;
using CompucareWard.Models;
using System.Text.RegularExpressions;
using CoreGraphics;
using CompucareWard.iOS.Helpers;
using CompucareWard.Views;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CompucareWard.iOS.Renderers.TabRenderer))]
namespace CompucareWard.iOS.Renderers
{
    public class TabRenderer : TabbedRenderer
    {
        public TabRenderer()
        {

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var pages = this.ViewController.ChildViewControllers.OfType<IVisualElementRenderer>().Select(e => e.Element as Page).ToArray();
            var tabs = Element as TabbedPage;

            if (pages.Length != this.TabBar.Items.Length)
                throw new InvalidOperationException("Number of Pages does not match the Number of Tabitems");

            for (var i = 0; i < pages.Length; i++)
            {
                var tabItem = this.TabBar.Items[i];
                Page page = pages[i] is NavigationPage navPage ? navPage.CurrentPage : pages[i];

                if (tabItem.Image == null)
                {
                    tabItem.Image = ImageHelper.ImageFromFont(SHProperties.GetIcon(page), UIColor.Black, new CGSize(40, 30));
                    tabItem.SelectedImage = ImageHelper.ImageFromFont(SHProperties.GetIcon(page), UIColor.Black, new CGSize(40, 30));
                }                                

                (tabs.Children[i] as NavigationPage).CurrentPage.PropertyChanged += TabbarPageRenderer_PropertyChanged;
            }         
        }

        private void TabbarPageRenderer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var page = sender as Page;

            if (page == null)
                return;

            if (e.PropertyName == nameof(RemindersPage.BadgeNumber))
            {
                if (CheckValidTabIndex(page, out int tabIndex))
                    UpdateBadge(TabBar.Items[2], (page as RemindersPage).BadgeNumber);                    

                return;
            }
        }

        public bool CheckValidTabIndex(Page page, out int tabIndex)
        {
            tabIndex = Tabbed.Children.IndexOf(page);
            return tabIndex < TabBar.Items.Length;
        }      

        private void UpdateBadge(UITabBarItem item, int badge)
        {
            if (badge == 0)
            {
                item.BadgeValue = null;
                item.BadgeColor = null;
            }
            else
            {
                item.BadgeValue = badge.ToString();
                item.BadgeColor = UIColor.Red;
            }
        }

    }
}
