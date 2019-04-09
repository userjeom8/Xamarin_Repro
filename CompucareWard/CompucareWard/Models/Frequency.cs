using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class Frequency : BindableBase
    {
        private int _minutes;
        private string _name;

        public Frequency()
        {

        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }
    }
}
