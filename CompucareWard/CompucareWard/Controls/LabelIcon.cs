using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CompucareWard.Controls
{
    public class LabelIcon : Label
    {
        string Typeface { get; } = Device.RuntimePlatform == Device.iOS ? "Font Awesome 5 Pro" : "Assets/fa-light-300.ttf#Font Awesome 5 Pro";

        public LabelIcon()
        {
            FontFamily = Typeface;
        }
    }
}
