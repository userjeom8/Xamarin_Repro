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

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CompucareWard.iOS.Renderers.CustomNavigationPageRenderer))]
namespace CompucareWard.iOS.Renderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        public CustomNavigationPageRenderer()
        {

        }

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            base.PushViewController(viewController, animated);

            var currentPage = (this.Element as NavigationPage)?.CurrentPage;

            if (this.NavigationBar == null || currentPage == null)
                return;

            if (SHProperties.GetUser(currentPage) is User user)
            {
                // create new button
                var userButton = new UIButton(UIButtonType.Custom);

                if (user.Thumbnail != null)
                {
                    var imageData = NSData.FromArray(user.Thumbnail);
                    var uiimage = UIImage.LoadFromData(imageData);
                    userButton.Frame = new CGRect(0, 0, 40, 40);
                    var rect = new CGRect(0, 0, userButton.Frame.Size.Width, userButton.Frame.Size.Height);
                    UIBezierPath.FromRoundedRect(rect, cornerRadius: rect.Width / 2).AddClip();
                    uiimage.DrawAsPatternInRect(rect);
                    userButton.BackgroundColor = new UIColor(patternImage: uiimage);
                    //userButton.SetImage(uiimage, UIControlState.Normal);
                    //userButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;                   
                    //userButton.ImageView.Layer.MasksToBounds = true;                    
                }
                else
                {
                    userButton.SetTitle(user.Initials, UIControlState.Normal);
                    userButton.Layer.BackgroundColor = UIColor.White.CGColor;
                    userButton.SetTitleColor(UIKit.UIColor.Black, UIControlState.Normal);
                    userButton.Frame = new CGRect(0, 0, 40, 40); // set this to the size of the image            
                }

                userButton.Layer.MasksToBounds = true;
                userButton.Layer.CornerRadius = userButton.Bounds.Size.Width / 2;
                userButton.Layer.BorderWidth = 2;
                userButton.Layer.BorderColor = UIColor.White.CGColor;

                // add button to toolbar
                var rightBarButton = new UIBarButtonItem(userButton);
                TopViewController.NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { rightBarButton };
            }

            var buttonItems = TopViewController.NavigationItem.RightBarButtonItems;
            var toolbarItems = currentPage.ToolbarItems.OrderByDescending(t => currentPage.ToolbarItems.IndexOf(t)).ToList();

            foreach (var toolbarItem in toolbarItems)
            {
                //if (SHProperties.GetIcon(toolbarItem) is string icon)
                //{
                //    var nativeBarItem = buttonItems[toolbarItems.IndexOf(toolbarItem)];
                //    nativeBarItem.Image = ImageHelper.ImageFromFont(icon, UIColor.Black, new CGSize(30, 30));
                //}
                if (SHProperties.GetIsLeftToolbarItem(toolbarItem))
                {
                    var nativeBarItem = buttonItems[toolbarItems.IndexOf(toolbarItem)];
                    TopViewController.NavigationItem.RightBarButtonItems = TopViewController.NavigationItem.RightBarButtonItems.Where(t => t != nativeBarItem).ToArray();
                    TopViewController.NavigationItem.LeftBarButtonItems = (TopViewController.NavigationItem.LeftBarButtonItems ?? new UIBarButtonItem[0]).Concat(new List<UIBarButtonItem> { nativeBarItem }).ToArray();
                }
            }
        }
    }
}
