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

[assembly: ExportRenderer(typeof(Picker), typeof(CompucareWard.iOS.Renderers.StandardPickerRenderer))]
namespace CompucareWard.iOS.Renderers
{
    public class StandardPickerRenderer : PickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                var picker = sender as Picker;
                var toolbar = Control?.InputAccessoryView as UIToolbar;

                if (picker?.StyleId == "NextPrevious" && toolbar != null && toolbar.Items.Length == 2)
                {
                    var keyboardButtons = SHProperties.GetKeyboardButtons(picker);
                    var spacerButton = toolbar.Items[0];

                    var prevButton = new UIBarButtonItem(ImageHelper.ImageFromFont(SHIcon.ChevronUp, UIColor.Black, new CGSize(28, 25)), UIBarButtonItemStyle.Done, (o, a) =>
                    {
                        MessagingCenter.Send(picker, "Previous");
                    });

                    prevButton.Enabled = keyboardButtons == Controls.Enums.KeyboardButton.Both || keyboardButtons == Controls.Enums.KeyboardButton.Prev || keyboardButtons == Controls.Enums.KeyboardButton.BothNoClear;

                    var nextButton = new UIBarButtonItem(ImageHelper.ImageFromFont(SHIcon.ChevronDown, UIColor.Black, new CGSize(28, 25)), UIBarButtonItemStyle.Done, (o, a) =>
                    {
                        MessagingCenter.Send(picker, "Next");
                    });

                    nextButton.Enabled = keyboardButtons == Controls.Enums.KeyboardButton.Both || keyboardButtons == Controls.Enums.KeyboardButton.Next || keyboardButtons == Controls.Enums.KeyboardButton.BothNoClear;

                    var clearButton = new UIBarButtonItem("Clear", UIBarButtonItemStyle.Done, (o, a) =>
                    {
                        MessagingCenter.Send(picker, "Clear");
                    });

                    if (keyboardButtons == Controls.Enums.KeyboardButton.BothNoClear)
                        toolbar.SetItems(new[] { prevButton, nextButton }, false);
                    else if (keyboardButtons == Controls.Enums.KeyboardButton.None)
                        toolbar.SetItems(new[] { spacerButton, clearButton }, false);
                    else
                        toolbar.SetItems(new[] { prevButton, nextButton, spacerButton, clearButton }, false);
                }
            }
        }
    }
}
