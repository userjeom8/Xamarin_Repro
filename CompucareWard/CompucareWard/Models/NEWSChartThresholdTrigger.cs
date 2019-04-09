using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class NEWSChartThresholdTrigger : BindableBase
    {
        private int _highLimit;
        private int _lowLimit;
        private int _observationFrequencyInMinutes;
        private string _responseText;
        private bool _has3InSingleResult;

        [JsonProperty(PropertyName = "lowlimit")]
        public int LowLimit
        {
            get => _lowLimit;
            set => SetProperty(ref _lowLimit, value);
        }

        [JsonProperty(PropertyName = "highlimit")]
        public int HighLimit
        {
            get => _highLimit;
            set => SetProperty(ref _highLimit, value);
        }

        [JsonProperty(PropertyName = "observationfrequencyinminutes")]
        public int ObservationFrequencyInMinutes
        {
            get => _observationFrequencyInMinutes;
            set => SetProperty(ref _observationFrequencyInMinutes, value);
        }

        [JsonProperty(PropertyName = "responsetext")]
        public string ResponseText
        {
            get => _responseText;
            set => SetProperty(ref _responseText, value);
        }

        [JsonProperty(PropertyName = "has3insingleresult")]
        public bool Has3InSingleResult
        {
            get => _has3InSingleResult;
            set => SetProperty(ref _has3InSingleResult, value);
        }
    }
}
