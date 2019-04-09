using CompucareWard.Models.Converters;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace CompucareWard.Models
{
    public class FormComponentResultSimplified : BindableBase
    {
        private int _formComponentResultId;
        private int _formResultId;
        private int _formComponentId;
        private string _result;
        private string _caption;
        private string _header;
        private int _parentFormComponentResultId;
        private string _resultUnits;
        private string _warningColour;
        private ObservableCollection<TestResultOptionDTO> _resultOptions;
        private string _unitAbbreviation;

        [JsonProperty(PropertyName = "formcomponentresultid")]
        public int FormComponentResultId
        {
            get => _formComponentResultId;
            set => SetProperty(ref _formComponentResultId, value);
        }

        [JsonProperty(PropertyName = "formresultid")]
        public int FormResultId
        {
            get => _formResultId;
            set => SetProperty(ref _formResultId, value);
        }

        [JsonProperty(PropertyName = "formcomponentid")]
        public int FormComponentId
        {
            get => _formComponentId;
            set => SetProperty(ref _formComponentId, value);
        }

        [JsonProperty(PropertyName = "result")]
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        [JsonProperty(PropertyName = "caption")]
        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        [JsonProperty(PropertyName = "header")]
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        [JsonProperty(PropertyName = "parentformcomponentresultid")]
        public int ParentFormComponentResultId
        {
            get => _parentFormComponentResultId;
            set => SetProperty(ref _parentFormComponentResultId, value);
        }

        [JsonProperty(PropertyName = "resultunits")]
        public string ResultUnits
        {
            get => _resultUnits;
            set => SetProperty(ref _resultUnits, value, () => UnitAbbreviation = XElement.Parse(_resultUnits).Elements().FirstOrDefault(e => e.Name == "UnitAbbreviation" || e.Name == "CustomUnit")?.Value);
        }

        public string UnitAbbreviation
        {
            get => _unitAbbreviation;
            set => SetProperty(ref _unitAbbreviation, value);
        }

        [JsonProperty(PropertyName = "warningcolour")]
        public string WarningColour
        {
            get => _warningColour;
            set => SetProperty(ref _warningColour, value);
        }

        [JsonProperty(PropertyName = "resultoptions")]
        [JsonConverter(typeof(XMLConverter<ObservableCollection<TestResultOptionDTO>>))]
        public ObservableCollection<TestResultOptionDTO> ResultOptions
        {
            get => _resultOptions;
            set => SetProperty(ref _resultOptions, value, () => RaisePropertyChanged(nameof(ResultForDisplay)));
        }

        public string ResultForDisplay => ResultOptions?.Any() == true ? GetAbbreviation() : Result;

        string GetAbbreviation()
        {
            if (ResultOptions.FirstOrDefault(ro => ro.Value == (int.TryParse(Result, out int resultValue) ? resultValue : int.MaxValue))?.Caption is string abbreviation)
                return abbreviation?.IndexOf(" ") is int spaceIndex && spaceIndex >= 2 && spaceIndex <= 3 ? abbreviation?.Substring(0, spaceIndex) : abbreviation?.Substring(0, 1);
            else
                return null;
        }
    }
}
