using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class TestResultOptionDTO : BindableBase
    {
        private int _value;
        private string _caption;
        private Guid? _parentId;
        private Guid _testResultOptionId;

        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        public Guid? ParentId
        {
            get => _parentId;
            set => SetProperty(ref _parentId, value);
        }

        public Guid TestResultOptionId
        {
            get => _testResultOptionId;
            set => SetProperty(ref _testResultOptionId, value);
        }
    }
}