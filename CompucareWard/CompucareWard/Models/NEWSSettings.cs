using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CompucareWard.Models
{
    public class NEWSSettings : BindableBase
    {
        private string _guidanceDocumentURL;
        private ObservableCollection<NEWSChartThresholdTrigger> _newsChartThresholdsTriggers;

        [JsonProperty(PropertyName = "guidancedocumenturl")]
        public string GuidanceDocumentURL
        {
            get => _guidanceDocumentURL;
            set => SetProperty(ref _guidanceDocumentURL, value);
        }

        [JsonProperty(PropertyName = "newschartthresholdstriggers")]
        public ObservableCollection<NEWSChartThresholdTrigger> NEWSChartThresholdsTriggers
        {
            get => _newsChartThresholdsTriggers;
            set => SetProperty(ref _newsChartThresholdsTriggers, value);
        }
    }
}
