using CompucareWard.Controls;
using CompucareWard.Controls.AttachedProperties;
using CompucareWard.iOS.Helpers;
using CoreGraphics;
using System;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CompucareWard.iOS.Renderers.StandardEntryRenderer))]
namespace CompucareWard.iOS.Renderers
{
    public class StandardEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                var entry = sender as Entry;

                if (Control.InputAccessoryView == null && entry?.ReturnType == ReturnType.Next)
                {
                    var keyboardWidth = UIScreen.MainScreen.Bounds.Width;
                    var accessoryView = new UIToolbar(new RectangleF(0, 0, (float)keyboardWidth, 44)) { BarStyle = UIBarStyle.Default, Translucent = true };
                    var keyboardButtons = SHProperties.GetKeyboardButtons(entry);
                    var prevButton = new UIBarButtonItem(ImageHelper.ImageFromFont(SHIcon.ChevronUp, UIColor.Black, new CGSize(28, 25)), UIBarButtonItemStyle.Done, (o, a) =>
                    {
                        MessagingCenter.Send(entry, "Previous");
                    });
                    prevButton.Enabled = keyboardButtons == Controls.Enums.KeyboardButton.Both || keyboardButtons == Controls.Enums.KeyboardButton.Prev || keyboardButtons == Controls.Enums.KeyboardButton.BothNoClear;
                    var nextButton = new UIBarButtonItem(ImageHelper.ImageFromFont(SHIcon.ChevronDown, UIColor.Black, new CGSize(28, 25)), UIBarButtonItemStyle.Done, (o, a) =>
                    {
                        MessagingCenter.Send(entry, "Next");
                    });
                    nextButton.Enabled = keyboardButtons == Controls.Enums.KeyboardButton.Both || keyboardButtons == Controls.Enums.KeyboardButton.Next || keyboardButtons == Controls.Enums.KeyboardButton.BothNoClear;
                    accessoryView.SetItems(new[] { prevButton, nextButton }, false);
                    Control.InputAccessoryView = accessoryView;
                }
            }
        }
    }
}
