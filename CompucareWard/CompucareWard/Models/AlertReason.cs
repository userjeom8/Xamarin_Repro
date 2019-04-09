using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class AlertReason : BindableBase
    {
        private int _alertReasonId;
        private int _type;
        private int _severity;
        private string _name;
        private CodeTable _ingredient;

        [JsonProperty(PropertyName = "alertreasonid")]
        public int AlerReasonId
        {
            get => _alertReasonId;
            set => SetProperty(ref _alertReasonId, value);
        }

        [JsonProperty(PropertyName = "type")]
        public int Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public string TypeDescription
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return "Type: General";
                    case 1:
                        return "Type: Financial";
                    case 2:
                        return "Type: Clinical";
                    default:
                        return "Type: Allergy";
                }
            }
        }

        [JsonProperty(PropertyName = "severity")]
        public int Severity
        {
            get => _severity;
            set => SetProperty(ref _severity, value);
        }

        public string Detail
        {
            get
            {
                switch (Severity)
                {
                    case 0:
                        return $"{TypeDescription} | Severity: Low";
                    case 1:
                        return $"{TypeDescription} | Severity: Medium";
                    default:
                        return $"{TypeDescription} | Severity: High";
                }
            }
        }

        public string Description => Ingredient != null ? $"{Name} ({Ingredient.Name})" : Name;

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [JsonProperty(PropertyName = "ingredient")]
        public CodeTable Ingredient
        {
            get => _ingredient;
            set => SetProperty(ref _ingredient, value);
        }
    }
}
