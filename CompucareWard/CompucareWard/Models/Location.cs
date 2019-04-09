using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CompucareWard.Models
{
    public class Location : BindableBase
    {
        private int _locationId;
        private Person _residentMedicalOfficer;
        private int _siteId;

        [JsonProperty(PropertyName = "locationid")]
        public int LocationId
        {
            get => _locationId;
            set => SetProperty(ref _locationId, value);
        }

        [JsonProperty(PropertyName = "siteid")]
        public int SiteId
        {
            get => _siteId;
            set => SetProperty(ref _siteId, value);
        }

        [JsonProperty(PropertyName = "residentmedicalofficer")]
        public Person ResidentMedicalOfficer
        {
            get => _residentMedicalOfficer;
            set => SetProperty(ref _residentMedicalOfficer, value);
        }
    }
}
