using CompucareWard.Controls.AttachedProperties;
using CompucareWard.Controls.Enums;
using CompucareWard.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CompucareWard.Controls
{
    public abstract class NEWSViewCell<T> : ContentView, IEditorChild
        where T : View
    {
        public static readonly BindableProperty ComponentProperty = BindableProperty.Create(nameof(Component), typeof(object), typeof(NEWSViewCell<T>), defaultBindingMode: BindingMode.OneWay);

        public object Component
        {
            get => (object)GetValue(ComponentProperty);
            set => SetValue(ComponentProperty, value);
        }

        public static readonly BindableProperty PreviousEntryProperty = BindableProperty.Create(nameof(PreviousEntry), typeof(IEditorChild), typeof(NEWSViewCell<T>), defaultBindingMode: BindingMode.OneTime,
            propertyChanged: OnEntriesChanged);

        public IEditorChild PreviousEntry
        {
            get => (IEditorChild)GetValue(PreviousEntryProperty);
            set => SetValue(PreviousEntryProperty, value);
        }

        public static readonly BindableProperty NextEntryProperty = BindableProperty.Create(nameof(NextEntry), typeof(IEditorChild), typeof(NEWSViewCell<T>), defaultBindingMode: BindingMode.OneTime,
            propertyChanged: OnEntriesChanged);

        public IEditorChild NextEntry
        {
            get => (IEditorChild)GetValue(NextEntryProperty);
            set => SetValue(NextEntryProperty, value);
        }

        static void OnEntriesChanged<IEditorChild>(BindableObject bindable, IEditorChild oldValue, IEditorChild newValue)
        {
            if (bindable is NEWSViewCell<T> newsViewCell)
            {
                if (newsViewCell.NextEntry != null && newsViewCell.PreviousEntry != null)
                    newsViewCell.Editor.SetValue(SHProperties.KeyboardButtonsProperty, KeyboardButton.Both);
                else if (newsViewCell.NextEntry != null)
                    newsViewCell.Editor.SetValue(SHProperties.KeyboardButtonsProperty, KeyboardButton.Next);
                else
                    newsViewCell.Editor.SetValue(SHProperties.KeyboardButtonsProperty, KeyboardButton.Prev);
            }
        }

        public static readonly BindableProperty ResultProperty = BindableProperty.Create(nameof(Result), typeof(string), typeof(NEWSViewCell<T>), propertyChanged: OnResultPropertiesChanged);

        public string Result
        {
            get => (string)GetValue(ResultProperty);
            set => SetValue(ResultProperty, value);
        }

        public static readonly BindableProperty ResultToMatchProperty = BindableProperty.Create(nameof(ResultToMatch), typeof(string), typeof(NEWSViewCell<T>), propertyChanged: OnResultPropertiesChanged);

        public string ResultToMatch
        {
            get => (string)GetValue(ResultToMatchProperty);
            set => SetValue(ResultToMatchProperty, value);
        }

        static void OnResultPropertiesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is NEWSViewCell<T> control)
            {
                if (control.Result == control.ResultToMatch)
                {
                    control.IsEnabled = true;
                    control.Opacity = 1;
                }
                else
                {
                    control.IsEnabled = false;
                    control.Opacity = 0.4;
                }
            }
        }

        protected abstract T Editor { get; }

        public View EditorChild => Editor;

        public void FocusControl(FocusDirection? focusDirection)
        {
            if (this.IsEnabled)
            {
                Editor.Focus();
            }
            else if (focusDirection == FocusDirection.Back && PreviousEntry != null)
            {
                PreviousEntry.FocusControl(focusDirection);
            }
            else if (focusDirection == FocusDirection.Forward && NextEntry != null)
            {
                NextEntry.FocusControl(focusDirection);
            }
        }

        public NEWSViewCell()
        {
            MessagingCenter.Subscribe<T>(this, "Previous", (p) =>
            {
                if (p == Editor && PreviousEntry != null)
                    PreviousEntry.FocusControl(FocusDirection.Back);
            });

            MessagingCenter.Subscribe<T>(this, "Next", (p) =>
            {
                if (p == Editor && NextEntry != null)
                {
                    NextEntry.FocusControl(FocusDirection.Forward);
                }
            });
        }
    }
}
