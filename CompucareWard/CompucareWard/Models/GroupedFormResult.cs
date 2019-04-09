using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace CompucareWard.Models
{
    public class GroupedFormResult : BindableBase
    {
        private string _caption;
        private string _unitAbbreviation;
        private ObservableCollection<SimpleResult> _results;

        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        public string UnitAbbreviation
        {
            get => _unitAbbreviation;
            set => SetProperty(ref _unitAbbreviation, value);
        }

        SimpleResult GetResult(int value) => Results.Skip(value).FirstOrDefault();

        public SimpleResult Value1 => GetResult(0);
        public SimpleResult Value2 => GetResult(1);
        public SimpleResult Value3 => GetResult(2);
        public SimpleResult Value4 => GetResult(3);
        public SimpleResult Value5 => GetResult(4);
        public SimpleResult Value6 => GetResult(5);
        public SimpleResult Value7 => GetResult(6);
        public SimpleResult Value8 => GetResult(7);
        public SimpleResult Value9 => GetResult(8);
        public SimpleResult Value10 => GetResult(9);
        public SimpleResult Value11 => GetResult(10);
        public SimpleResult Value12 => GetResult(11);
        public SimpleResult Value13 => GetResult(12);
        public SimpleResult Value14 => GetResult(13);
        public SimpleResult Value15 => GetResult(14);
        public SimpleResult Value16 => GetResult(15);
        public SimpleResult Value17 => GetResult(16);
        public SimpleResult Value18 => GetResult(17);
        public SimpleResult Value19 => GetResult(18);
        public SimpleResult Value20 => GetResult(19);
        public SimpleResult Value21 => GetResult(20);
        public SimpleResult Value22 => GetResult(21);
        public SimpleResult Value23 => GetResult(22);
        public SimpleResult Value24 => GetResult(23);

        public ObservableCollection<SimpleResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }
    }

    public class SimpleResult : BindableBase
    {
        private DateTime _dateTime;
        private FormComponentResultSimplified _formComponentResult;

        public DateTime DateTime
        {
            get => _dateTime;
            set => SetProperty(ref _dateTime, value);
        }

        public FormComponentResultSimplified FormComponentResult
        {
            get => _formComponentResult;
            set => SetProperty(ref _formComponentResult, value);
        }
    }
}
