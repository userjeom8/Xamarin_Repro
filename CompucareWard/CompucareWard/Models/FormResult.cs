using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace CompucareWard.Models
{
    public class FormResult : BindableBase
    {
        private int _formResultId;
        private int _patientId;
        private int? _formId;
        private DateTime? _dateTaken;
        private string _result;
        private string _warningText;
        private int _createLocationId;
        private int _createUserId;
        private int? _signOffUserId;
        private int? _episodeOfCareId;
        private int? _commonBookingId;
        private ObservableCollection<FormComponentResult> _formComponentResults;
        private ObservableCollection<FormComponentResult> _spoComponents;
        private FormComponentResult _selectedSPOComponent;
        private DateTime _createDate;
        private ObservableCollection<string> _errors = new ObservableCollection<string>();
        private bool _isValid = true;
        private bool _eventsHookupUp;
        private NEWSSettings _settings;

        private FormComponentResult _airOrOxygenComponent => FormComponentResults.Select(f => f.ChildFormComponentResults.SingleOrDefault(fc => fc.FormComponentSystemTypeId == 56)).SingleOrDefault();
        private FormComponentResult _oxygenSupplementComponent => FormComponentResults.Select(f => f.ChildFormComponentResults.SingleOrDefault(fc => fc.FormComponentSystemTypeId == 60)).SingleOrDefault();
        private FormComponentResult _oxygenDeviceComponent => FormComponentResults.Select(f => f.ChildFormComponentResults.SingleOrDefault(fc => fc.FormComponentSystemTypeId == 57)).SingleOrDefault();
        private FormComponentResult _spo1Component => FormComponentResults.Select(f => f.ChildFormComponentResults.SingleOrDefault(fc => fc.FormComponentSystemTypeId == 7)).SingleOrDefault();
        private FormComponentResult _spo2AirComponent => FormComponentResults.Select(f => f.ChildFormComponentResults.SingleOrDefault(fc => fc.FormComponentSystemTypeId == 54)).SingleOrDefault();
        private FormComponentResult _spo2OxygenComponent => FormComponentResults.Select(f => f.ChildFormComponentResults.SingleOrDefault(fc => fc.FormComponentSystemTypeId == 55)).SingleOrDefault();

        [JsonProperty(PropertyName = "formresultid")]
        public int FormResultId
        {
            get => _formResultId;
            set => SetProperty(ref _formResultId, value);
        }

        [JsonProperty(PropertyName = "patientid")]
        public int PatientId
        {
            get => _patientId;
            set => SetProperty(ref _patientId, value);
        }

        [JsonProperty(PropertyName = "formid")]
        public int? FormId
        {
            get => _formId;
            set => SetProperty(ref _formId, value);
        }

        [JsonProperty(PropertyName = "datetaken")]
        public DateTime? DateTaken
        {
            get => _dateTaken;
            set => SetProperty(ref _dateTaken, value);
        }

        [JsonProperty(PropertyName = "createdate")]
        public DateTime CreateDate
        {
            get => _createDate;
            set => SetProperty(ref _createDate, value);
        }

        [JsonProperty(PropertyName = "result")]
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        [JsonProperty(PropertyName = "warningtext")]
        public string WarningText
        {
            get => _warningText;
            set => SetProperty(ref _warningText, value);
        }

        [JsonProperty(PropertyName = "createuserid")]
        public int CreateUserId
        {
            get => _createUserId;
            set => SetProperty(ref _createUserId, value);
        }

        [JsonProperty(PropertyName = "signoffuserid")]
        public int? SignOffUserId
        {
            get => _signOffUserId;
            set => SetProperty(ref _signOffUserId, value);
        }

        [JsonProperty(PropertyName = "createlocationid")]
        public int CreateLocationId
        {
            get => _createLocationId;
            set => SetProperty(ref _createLocationId, value);
        }

        [JsonProperty(PropertyName = "episodeofcareid")]
        public int? EpisodeOfCareId
        {
            get => _episodeOfCareId;
            set => SetProperty(ref _episodeOfCareId, value);
        }

        [JsonProperty(PropertyName = "commonbookingid")]
        public int? CommonBookingId
        {
            get => _commonBookingId;
            set => SetProperty(ref _commonBookingId, value);
        }

        [JsonProperty(PropertyName = "formcomponentresults")]
        public ObservableCollection<FormComponentResult> FormComponentResults
        {
            get => _formComponentResults;
            set => SetProperty(ref _formComponentResults, value, () => InitialiseSPOComponents());
        }

        public ObservableCollection<FormComponentResult> SPOComponents
        {
            get => _spoComponents;
            set => SetProperty(ref _spoComponents, value);
        }

        public FormComponentResult SelectedSPOComponent
        {
            get => _selectedSPOComponent;
            set => SetProperty(ref _selectedSPOComponent, value, OnSelectedSPOComponentChanged);
        }

        [JsonIgnore]
        public ObservableCollection<string> Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }

        [JsonIgnore]
        public NEWSChartThresholdTrigger ThresholdTrigger
        {
            get
            {
                NEWSChartThresholdTrigger trigger = null;

                if (Settings?.NEWSChartThresholdsTriggers != null && decimal.TryParse(FormComponentResults.FirstOrDefault().Result, out decimal decimalValue))
                {
                    trigger = Settings.NEWSChartThresholdsTriggers.FirstOrDefault(tt => tt.LowLimit <= decimalValue && tt.HighLimit >= decimalValue);
                }

                return trigger;
            }
        }

        [JsonIgnore]
        public NEWSSettings Settings
        {
            get => _settings;
            set => SetProperty(ref _settings, value);
        }

        [JsonIgnore]
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        void InitialiseSPOComponents()
        {
            SPOComponents = new ObservableCollection<FormComponentResult> { _spo1Component, _spo2AirComponent, _spo2OxygenComponent };
            SelectedSPOComponent = SPOComponents.FirstOrDefault();
            InitialiseSelectedSPOComponent();
            _airOrOxygenComponent.AllowBlank = false;
        }

        public void HookupEvents()
        {
            if (_airOrOxygenComponent != null && !_eventsHookupUp)
            {
                _eventsHookupUp = true;
                _airOrOxygenComponent.PropertyChanged += _airOrOxygenComponent_PropertyChanged;
            }
        }

        public void UnHookEvents()
        {
            if (_airOrOxygenComponent != null && _eventsHookupUp)
            {
                _eventsHookupUp = false;
                _airOrOxygenComponent.PropertyChanged -= _airOrOxygenComponent_PropertyChanged;
            }
        }

        void OnSelectedSPOComponentChanged()
        {
            var oldSelectedItem = SPOComponents.SingleOrDefault(c => !c.AllowBlank);

            if (SelectedSPOComponent != null)
            {
                SelectedSPOComponent.AllowBlank = false;
                SelectedSPOComponent.Result = oldSelectedItem?.Result;
                SelectedSPOComponent.ResultOption = oldSelectedItem?.ResultOption;
                InitialiseSelectedSPOComponent();
            }

            if (oldSelectedItem != null)
            {
                oldSelectedItem.AllowBlank = true;
                oldSelectedItem.Result = null;
                oldSelectedItem.ResultOption = null;
            }
        }

        void InitialiseSelectedSPOComponent()
        {
            _airOrOxygenComponent.AllowBlank = false;

            if (SelectedSPOComponent == _spo2AirComponent)
                _airOrOxygenComponent.ResultOption = _airOrOxygenComponent.ResultOptions.Single(ro => ro.Value == 0);
            else if (SelectedSPOComponent == _spo2OxygenComponent)
                _airOrOxygenComponent.ResultOption = _airOrOxygenComponent.ResultOptions.Single(ro => ro.Value == 1);
            else
            {
                _airOrOxygenComponent.ResultOption = null;
                _airOrOxygenComponent.Result = null;
                _airOrOxygenComponent.ClearValidation();                          
            }                
        }

        private void _airOrOxygenComponent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FormComponentResult.ResultOption))
            {
                if (_airOrOxygenComponent.ResultOption?.Value == 1)
                {
                    _oxygenSupplementComponent.AllowBlank = false;
                    _oxygenDeviceComponent.AllowBlank = false;
                }
                else
                {
                    _oxygenSupplementComponent.AllowBlank = true;
                    _oxygenSupplementComponent.ResultOption = null;
                    _oxygenSupplementComponent.Result = null;
                    _oxygenDeviceComponent.AllowBlank = true;
                    _oxygenDeviceComponent.ResultOption = null;
                    _oxygenDeviceComponent.Result = null;
                }
            }
        }

        public bool Validate()
        {
            Errors.Clear();
            Errors = new ObservableCollection<string>(FormComponentResults.SelectMany(f => f.ChildFormComponentResults).Where(v => !v.Validate()).SelectMany(v => v.Errors));
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
