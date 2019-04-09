using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class Patient : BindableBase
    {
        private string _fullnameReverse;
        private DateTime? _dateOfBirth;
        private int _gender;
        private int _patientId;
        private bool _hasAlerts;
        private int _status;
        private string _statusColour;

        [JsonProperty(PropertyName = "patientid")]
        public int PatientId
        {
            get => _patientId;
            set => SetProperty(ref _patientId, value);
        }

        [JsonProperty(PropertyName = "fullnamereverse")]
        public string FullnameReverse
        {
            get => _fullnameReverse;
            set => SetProperty(ref _fullnameReverse, value);
        }

        [JsonProperty(PropertyName = "dateofbirth")]
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => SetProperty(ref _dateOfBirth, value);
        }

        [JsonIgnore()]
        public int? Age
        {
            get => DateOfBirth.HasValue ? Years(DateOfBirth.Value) : null as int?;
        }

        [JsonProperty(PropertyName = "hasalerts")]
        public bool HasAlerts
        {
            get => _hasAlerts;
            set => SetProperty(ref _hasAlerts, value);
        }

        [JsonProperty(PropertyName = "status")]
        public int Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        [JsonProperty(PropertyName = "statuscolour")]
        public string StatusColour
        {
            get => _statusColour;
            set => SetProperty(ref _statusColour, value);
        }

        [JsonProperty(PropertyName = "gender")]
        public int Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public string GenderDescription
        {
            get
            {
                switch (Gender)
                {
                    case 1:
                        return "Male";
                    case 2:
                        return "Female";
                    case 3:
                        return "Not Specified";
                    default:
                        return "Unknown";
                }
            }
        }

        int Years(DateTime start)
        {
            var end = DateTime.Today;

            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
    }
}
