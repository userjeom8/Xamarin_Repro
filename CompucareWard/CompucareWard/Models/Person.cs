using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class Person : BindableBase
    {
        private string _fullnameReverse;
        private int _id;
        private int _personId;

        [JsonProperty(PropertyName = "id")]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [JsonProperty(PropertyName = "personid")]
        public int PersonId
        {
            get => _personId;
            set => SetProperty(ref _personId, value);
        }

        [JsonProperty(PropertyName = "fullnamereverse")]
        public string FullnameReverse
        {
            get => _fullnameReverse;
            set => SetProperty(ref _fullnameReverse, value);
        }
    }
}
