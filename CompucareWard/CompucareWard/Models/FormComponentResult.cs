using CompucareWard.Models.Converters;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using CompucareWard.Validation;
using CompucareWard.Validation.Rules;

namespace CompucareWard.Models
{
    public class FormComponentResult : ValidationBase<string>
    {
        private int _formComponentResultId;
        private int _formResultId;
        private int _formComponentId;
        private string _result;
        private bool _isLocked;
        private string _caption;
        private string _header;
        private string _notes;
        private string _warningText;
        private string _externalIdentifier;
        private int _parentFormComponentResultId;
        private string _resultUnits;
        private ObservableCollection<TestResultOptionDTO> _resultOptions;
        private string _resultFormula;
        private bool _allowBlank = true;
        private int? _precision;
        private int? _lowLimit;
        private int? _highLimit;
        private string _resultOptionAbbreviation;
        private ObservableCollection<WarningBandDTO> _warningBands;
        private string _warningColour;
        private decimal? _warningResult;
        private ObservableCollection<FormComponentResult> _childFormComponentResults;
        private TestResultOptionDTO _resultOption;
        private bool _isVisible;
        private int _status;
        private FormComponentResult _parent;
        private ObservableCollection<TestResultOptionDTO> _resultOptionsWithNull;
        private int? _formComponentSystemTypeId;

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

        [JsonProperty(PropertyName = "status")]
        public int Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        [JsonProperty(PropertyName = "isvisible")]
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        [JsonProperty(PropertyName = "result")]
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value, ResultChanged);
        }

        [JsonProperty(PropertyName = "islocked")]
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
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

        [JsonProperty(PropertyName = "notes")]
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        [JsonProperty(PropertyName = "warningtext")]
        public string WarningText
        {
            get => _warningText;
            set => SetProperty(ref _warningText, value);
        }

        [JsonProperty(PropertyName = "externalidentifier")]
        public string ExternalIdentifier
        {
            get => _externalIdentifier;
            set => SetProperty(ref _externalIdentifier, value);
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
            set => SetProperty(ref _resultUnits, value);
        }

        [JsonProperty(PropertyName = "resultoptions")]
        [JsonConverter(typeof(XMLConverter<ObservableCollection<TestResultOptionDTO>>))]
        public ObservableCollection<TestResultOptionDTO> ResultOptions
        {
            get => _resultOptions;
            set => SetProperty(ref _resultOptions, value, () => ResultOptionsWithNull = new ObservableCollection<TestResultOptionDTO>((new List<TestResultOptionDTO> { null }).Concat(value)));
        }

        public ObservableCollection<TestResultOptionDTO> ResultOptionsWithNull
        {
            get => _resultOptionsWithNull;
            set => SetProperty(ref _resultOptionsWithNull, value);
        }

        public TestResultOptionDTO ResultOption
        {
            get => _resultOption = _resultOption ?? ResultOptions.FirstOrDefault(ro => ro.Value == (int.TryParse(Result, out int resultValue) ? resultValue : int.MaxValue));
            set => SetProperty(ref _resultOption, value, () => { Result = value?.Value.ToString(); ResultOptionAbbreviation = GetAbbreviation(value?.Caption); });
        }

        public string ResultOptionAbbreviation
        {
            get => _resultOptionAbbreviation = _resultOptionAbbreviation ?? GetAbbreviation(ResultOption?.Caption);
            set => SetProperty(ref _resultOptionAbbreviation, value);
        }

        string GetAbbreviation(string abbreviation) => abbreviation?.IndexOf(" ") is int spaceIndex && spaceIndex >= 2 && spaceIndex <= 3 ? abbreviation?.Substring(0, spaceIndex) : abbreviation?.Substring(0, 1);

        [JsonProperty(PropertyName = "resultformula")]
        public string ResultFormula
        {
            get => _resultFormula;
            set => SetProperty(ref _resultFormula, value);
        }

        [JsonProperty(PropertyName = "allowblank")]
        public bool AllowBlank
        {
            get => _allowBlank;
            set => SetProperty(ref _allowBlank, value);
        }

        [JsonProperty(PropertyName = "precision")]
        public int? Precision
        {
            get => _precision;
            set => SetProperty(ref _precision, value);
        }

        [JsonProperty(PropertyName = "lowlimit")]
        public int? LowLimit
        {
            get => _lowLimit;
            set => SetProperty(ref _lowLimit, value);
        }

        [JsonProperty(PropertyName = "highlimit")]
        public int? HighLimit
        {
            get => _highLimit;
            set => SetProperty(ref _highLimit, value);
        }

        [JsonProperty(PropertyName = "warningbands")]
        [JsonConverter(typeof(XMLConverter<ObservableCollection<WarningBandDTO>>))]
        public ObservableCollection<WarningBandDTO> WarningBands
        {
            get => _warningBands;
            set => SetProperty(ref _warningBands, value);
        }

        [JsonProperty(PropertyName = "warningcolour")]
        public string WarningColour
        {
            get => _warningColour;
            set => SetProperty(ref _warningColour, value);
        }

        [JsonProperty(PropertyName = "warningresult")]
        public decimal? WarningResult
        {
            get => _warningResult;
            set => SetProperty(ref _warningResult, value);
        }

        [JsonProperty(PropertyName = "formComponentSystemTypeId")]
        public int? FormComponentSystemTypeId
        {
            get => _formComponentSystemTypeId;
            set => SetProperty(ref _formComponentSystemTypeId, value);
        }

        [JsonProperty(PropertyName = "childformcomponentresults")]
        public ObservableCollection<FormComponentResult> ChildFormComponentResults
        {
            get => _childFormComponentResults;
            set => SetProperty(ref _childFormComponentResults, value, () => value?.ForEach(cfr => cfr.Parent = this));
        }

        [JsonIgnore]
        public FormComponentResult Parent
        {
            get => _parent;
            set => SetProperty(ref _parent, value);
        }

        [JsonIgnore]
        public int MaxLength
        {
            get
            {
                int? calculatedLength = null;

                if (LowLimit?.ToString().Length is int lowLimitLength)
                    calculatedLength = lowLimitLength;

                if (HighLimit?.ToString().Length is int highLimitLength && highLimitLength > calculatedLength)
                    calculatedLength = highLimitLength;

                if (calculatedLength.HasValue && Precision.HasValue && Precision > 0)
                    calculatedLength += Precision.Value + 1;

                return calculatedLength ?? int.MaxValue.ToString().Length;
            }
        }

        public FormComponentResult()
        {
            _validations.Add(new IsValidDecimal(nameof(Result)));
        }

        void ResultChanged()
        {
            WarningBandDTO matchingWarning = null;

            if (decimal.TryParse(Result, out decimal decimalResult))
            {
                foreach (var warning in WarningBands)
                {
                    var hasLowValue = decimal.TryParse(warning.LowValue, out decimal lowValue);
                    var hasHighValue = decimal.TryParse(warning.HighValue, out decimal highValue);

                    if ((hasLowValue && hasHighValue && decimalResult > lowValue && decimalResult < highValue) || (!hasHighValue && hasLowValue && decimalResult > lowValue)
                        || (!hasLowValue && hasHighValue && decimalResult < highValue))
                        matchingWarning = warning;
                }
            }

            WarningText = matchingWarning?.Text;
            WarningColour = matchingWarning?.Colour;
            WarningResult = matchingWarning?.Result;
            Parent?.ChildResultChanged();
        }

        void ChildResultChanged()
        {
            var systemTypeIds = new int[] { 6, 7, 55, 54, 56, 3, 9, 58, 8 };
            var resultsToCalculate = ChildFormComponentResults.Where(r => systemTypeIds.Contains(r.FormComponentSystemTypeId ?? -1)).ToArray();

            if (resultsToCalculate.All(r => string.IsNullOrEmpty(r.Result)))
                Result = null;
            else
                Result = resultsToCalculate.Select(r => r.WarningResult ?? 0).Sum().ToString();
        }

        public bool Validate() => Validate(Result);

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Precision) when Precision.HasValue:
                    _validations.Add(new PrecisionNotExceeded(nameof(Result), Precision.Value));
                    RaisePropertyChanged(nameof(MaxLength));
                    break;
                case nameof(AllowBlank) when !AllowBlank:
                    _validations.Add(new IsNotNullOrEmptyRule(nameof(Result)));
                    break;
                case nameof(AllowBlank) when AllowBlank && _validations.FirstOrDefault(v => v is IsNotNullOrEmptyRule) is IsNotNullOrEmptyRule nullRule:
                    _validations.Remove(nullRule);
                    break;
                case nameof(HighLimit) when HighLimit.HasValue:
                    _validations.Add(new IsNotMoreOrLessThan(nameof(Result), HighLimit.Value, true));
                    RaisePropertyChanged(nameof(MaxLength));
                    break;
                case nameof(LowLimit) when LowLimit.HasValue:
                    _validations.Add(new IsNotMoreOrLessThan(nameof(Result), LowLimit.Value, false));
                    RaisePropertyChanged(nameof(MaxLength));
                    break;
                case nameof(Result):
                    Validate();
                    break;
                default:
                    break;
            }
        }
    }
}
