using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CompucareWard.Controls.Effects
{
    public class UnderlineEffect : RoutingEffect
    {
        public const string EffectNamespace = "SHEffect";

        public UnderlineEffect() : base($"{EffectNamespace}.{nameof(UnderlineEffect)}")
        {

        }
    }
}
