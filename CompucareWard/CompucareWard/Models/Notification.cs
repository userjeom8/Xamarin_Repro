using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CompucareWard.Models
{
    public class Notification
    {
        private Patient _patient;        
        private string _locationName;
        private int _locationId;
        private string _bedName;
        private int _bedId;
        private int _oberservationFrequency;
        private DateTime _observationDue;
        private string _observationDueName;
        private string _minsFromNow;
        private bool _isDueTimeNegative;


        [JsonProperty(PropertyName = "patient")]
        public Patient Patient
        {
            get { return _patient; }
            set { _patient = value; }
        }

        [JsonProperty(PropertyName = "locationName")]
        public string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; }
        }

        [JsonProperty(PropertyName = "locationid")]
        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }

        [JsonProperty(PropertyName = "bedName")]
        public string BedName
        {
            get { return _bedName; }
            set { _bedName = value; }
        }

        [JsonProperty(PropertyName = "bedId")]
        public int BedId
        {
            get { return _bedId; }
            set { _bedId = value; }
        }        

        public bool IsDueTimeNegative
        {
            get { return _isDueTimeNegative;  }
            set { _isDueTimeNegative = value; }
        }

        public string MinsFromNow
        {
            get { return _minsFromNow; }
            set { _minsFromNow = value; }
        }

        public int OberservationFrequency
        {
            get { return _oberservationFrequency; }
            set { _oberservationFrequency = value; }
        }     

        public DateTime ObservationDue
        {
            get { return _observationDue; }
            set { _observationDue = value; }
        }

        public String ObservationDueName
        {
            get { return _observationDueName; }
            set { _observationDueName = value; }
        }
    }

    public class GroupedNotifications : ObservableCollection<InpatientBooking>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
