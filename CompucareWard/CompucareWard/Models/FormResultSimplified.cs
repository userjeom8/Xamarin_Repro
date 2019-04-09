using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CompucareWard.Models
{
    public class FormResultSimplified : BindableBase
    {
        private int _formResultId;
        private int _patientId;
        private DateTime? _dateTaken;
        private string _result;
        private int? _episodeOfCareId;
        private int? _commonBookingId;
        private ObservableCollection<FormComponentResultSimplified> _formComponentResults;
        private DateTime _createDate;

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
        public ObservableCollection<FormComponentResultSimplified> FormComponentResults
        {
            get => _formComponentResults;
            set => SetProperty(ref _formComponentResults, value);
        }
    }
}
