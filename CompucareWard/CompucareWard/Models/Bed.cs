using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class Bed : BindableBase
    {
        private string _name;
        private Location _location;
        private int _bedId;

        [JsonProperty(PropertyName = "bedid")]
        public int BedId
        {
            get => _bedId;
            set => SetProperty(ref _bedId, value);
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [JsonProperty(PropertyName = "location")]
        public Location Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }
    }
}
