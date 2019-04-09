using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CompucareWard.Models
{
    public class NextOfKin : BindableBase
    {
        private int _nextOfKinId;
        private string _fullnameReverse;
        private string _relationship;

        [JsonProperty(PropertyName = "nextofkinid")]
        public int NextOfKinId
        {
            get => _nextOfKinId;
            set => SetProperty(ref _nextOfKinId, value);
        }

        [JsonProperty(PropertyName = "fullnamereverse")]
        public string FullnameReverse
        {
            get => _fullnameReverse;
            set => SetProperty(ref _fullnameReverse, value);
        }

        [JsonProperty(PropertyName = "relationship")]
        public string Relationship
        {
            get => _relationship;
            set => SetProperty(ref _relationship, value);
        }
    }
}
