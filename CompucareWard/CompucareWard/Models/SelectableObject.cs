using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class SelectableObject<T> : BindableBase
        where T : BindableBase, new()
    {
        private bool _isSelected = true;
        private T _value;

        public SelectableObject(T value)
        {
            Value = value;
        }

        public T Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
