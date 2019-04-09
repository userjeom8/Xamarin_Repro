using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Effects = CompucareWard.Controls.Effects;

[assembly: ResolutionGroupName(Effects.UnderlineEffect.EffectNamespace)]
[assembly: ExportEffect(typeof(CompucareWard.iOS.UnderlineEffect), nameof(Effects.UnderlineEffect))]
namespace CompucareWard.iOS
{
    public class UnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var label = (Label)Element;
            UIStringAttributes attr = new UIStringAttributes();
            attr.UnderlineStyle = NSUnderlineStyle.Single;
            ((UILabel)Control).AttributedText = new NSAttributedString(label.Text, attr);
        }

        protected override void OnDetached()
        {
            var label = (Label)Element;
            UIStringAttributes attr = new UIStringAttributes();
            attr.UnderlineStyle = NSUnderlineStyle.None;
            ((UILabel)Control).AttributedText = new NSAttributedString(label.Text, attr);
        }
    }
}
