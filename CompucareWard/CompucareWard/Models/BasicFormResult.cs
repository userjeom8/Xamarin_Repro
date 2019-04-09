using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class BasicFormResult : BindableBase
    {
        private string _result;
        private string _warningColour;

        [JsonProperty(PropertyName = "result")]
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        [JsonProperty(PropertyName = "warningcolour")]
        public string WarningColour
        {
            get => _warningColour;
            set => SetProperty(ref _warningColour, value);
        }

        public decimal? DecimalResult
        {
            get => decimal.TryParse(Result, out decimal decimalResult) ? decimalResult : null as decimal?;
        }
    }
}
