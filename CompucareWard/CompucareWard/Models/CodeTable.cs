using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class CodeTable : BindableBase
    {
        private string _name;
        private int _id;

        [JsonProperty(PropertyName = "id")]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
