using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class Alert : BindableBase
    {
        private int _alertId;
        private DateTime _date;
        private DateTime? _dateExpired;
        private string _time;
        private string _notes;
        private int? _patientId;
        private AlertReason _alertReason;

        [JsonProperty(PropertyName = "alertid")]
        public int AlertId
        {
            get => _alertId;
            set => SetProperty(ref _alertId, value);
        }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        [JsonProperty(PropertyName = "dateexpired")]
        public DateTime? DateExpired
        {
            get => _dateExpired;
            set => SetProperty(ref _dateExpired, value);
        }

        [JsonProperty(PropertyName = "time")]
        public string Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        [JsonProperty(PropertyName = "notes")]
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        [JsonProperty(PropertyName = "patientid")]
        public int? PatientId
        {
            get => _patientId;
            set => SetProperty(ref _patientId, value);
        }

        [JsonProperty(PropertyName = "alertreason")]
        public AlertReason AlertReason
        {
            get => _alertReason;
            set => SetProperty(ref _alertReason, value);
        }
    }
}
