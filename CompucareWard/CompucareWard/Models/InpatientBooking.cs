using Newtonsoft.Json;
using Prism.Mvvm;
using System;

namespace CompucareWard.Models
{
    public class InpatientBooking : BindableBase
    {
        private int _inpatientBookingId;
        private Patient _patient;
        private BasicFormResult _newsCurrent;
        private BasicFormResult _newsPrevious;
        private int _episodeOfCareId;
        private Bed _currentBed;
        private DateTime _admission;
        private DateTime _discharge;
        private Person _responsibleNurse;
        private Person _attendingClinician;
        private int _commonBookingId;
        private int? _observationFrequencyInMinutes;
        private DateTime? _observationDue;

        [JsonProperty(PropertyName = "inpatientbookingid")]
        public int InpatientBookingId
        {
            get => _inpatientBookingId;
            set => SetProperty(ref _inpatientBookingId, value);
        }

        [JsonProperty(PropertyName = "episodeofcareId")]
        public int EpisodeOfCareId
        {
            get => _episodeOfCareId;
            set => SetProperty(ref _episodeOfCareId, value);
        }

        [JsonProperty(PropertyName = "commonbookingid")]
        public int CommonBookingId
        {
            get => _commonBookingId;
            set => SetProperty(ref _commonBookingId, value);
        }

        [JsonProperty(PropertyName = "patient")]
        public Patient Patient
        {
            get => _patient;
            set => SetProperty(ref _patient, value);
        }

        [JsonProperty(PropertyName = "attendingclinician")]
        public Person AttendingClinician
        {
            get => _attendingClinician;
            set => SetProperty(ref _attendingClinician, value);
        }

        [JsonProperty(PropertyName = "responsiblenurse")]
        public Person ResponsibleNurse
        {
            get => _responsibleNurse;
            set => SetProperty(ref _responsibleNurse, value);
        }

        [JsonProperty(PropertyName = "admission")]
        public DateTime Admission
        {
            get => _admission;
            set => SetProperty(ref _admission, value);
        }

        [JsonProperty(PropertyName = "discharge")]
        public DateTime Discharge
        {
            get => _discharge;
            set => SetProperty(ref _discharge, value);
        }

        [JsonProperty(PropertyName = "currentBed")]
        public Bed CurrentBed
        {
            get => _currentBed;
            set => SetProperty(ref _currentBed, value);
        }

        [JsonProperty(PropertyName = "newscurrent")]
        public BasicFormResult NEWSCurrent
        {
            get => _newsCurrent;
            set => SetProperty(ref _newsCurrent, value);
        }

        [JsonProperty(PropertyName = "newsprevious")]
        public BasicFormResult NEWSPrevious
        {
            get => _newsPrevious;
            set => SetProperty(ref _newsPrevious, value);
        }

        [JsonProperty(PropertyName = "observationFrequencyInMinutes")]
        public int? ObservationFrequencyInMinutes
        {
            get => _observationFrequencyInMinutes;
            set => SetProperty(ref _observationFrequencyInMinutes, value);
        }

        [JsonProperty(PropertyName = "observationDue")]
        public DateTime? ObservationDue
        {
            get => _observationDue;
            set => SetProperty(ref _observationDue, value);
        }

        public string Duration
        {
            get
            {
                switch (Discharge.Subtract(Admission))
                {
                    case TimeSpan days when days.Days >= 1:
                        return $"{days.ToString("%d")} night(s)";
                    case TimeSpan days when days.Days == 0:
                    default:
                        return $"{days.ToString("%h")} hour(s)  {days.ToString("%m")} minute(s)";
                }
            }
        }

        public bool? IsNEWSUp => NEWSCurrent?.DecimalResult != null && NEWSPrevious?.DecimalResult != null && NEWSCurrent.DecimalResult != NEWSPrevious.DecimalResult
                                 ? NEWSCurrent.DecimalResult.Value > NEWSPrevious.DecimalResult.Value
                                 : null as bool?;

        public bool IsReminder => ObservationFrequencyInMinutes != null && ObservationDue != null;

        public bool IsCurrentReminder => IsReminder && ObservationDue <= DateTime.Now;

        public string MinutesToObservation
        {
            get
            {
                if (!IsReminder)
                    return string.Empty;

                return ObservationDue.Value.Subtract(DateTime.Now) is TimeSpan duration && duration.Hours == 0 
                       ? $"{duration.Minutes}m" 
                       : $"{duration.Hours}h {(duration.Minutes < 0 ? duration.Minutes * -1 : duration.Minutes)}m";
            }
        }

        public bool IsDueTimeNegative => IsReminder && ObservationDue.Value.Subtract(DateTime.Now).TotalMinutes < 0;
    }
}