using CompucareWard.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CompucareWard.Controls
{
    public interface IEditorChild
    {
        void FocusControl(FocusDirection? focusDirection);
    }
}
