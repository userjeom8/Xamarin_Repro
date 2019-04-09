using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class Contact : BindableBase
    {
        private string _fullnameReverse;
        private string _type;
        private bool _isEmail;
        private int _personId;
        private string _value;

        [JsonProperty(PropertyName = "fullnamereverse")]
        public string FullnameReverse
        {
            get => _fullnameReverse;
            set => SetProperty(ref _fullnameReverse, value);
        }

        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        [JsonProperty(PropertyName = "isemail")]
        public bool IsEmail
        {
            get => _isEmail;
            set => SetProperty(ref _isEmail, value);
        }

        [JsonProperty(PropertyName = "personid")]
        public int PersonId
        {
            get => _personId;
            set => SetProperty(ref _personId, value);
        }

        [JsonProperty(PropertyName = "value")]
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
