using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace CompucareWard.Validation
{
    public class ValidationBase<T> : BindableBase
    {
        protected List<IValidationRule<T>> _validations { get; } = new List<IValidationRule<T>>();
        private ObservableCollection<string> _errors = new ObservableCollection<string>();
        private bool _isValid = true;

        [JsonIgnore]
        public ObservableCollection<string> Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }

        [JsonIgnore]
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public string ValidationDescriptions => Errors.Any() ? string.Join(Environment.NewLine, Errors) : string.Empty;

        protected bool Validate(T value)
        {
            IsValid = true;
            Errors.Clear();
            Errors = new ObservableCollection<string>(_validations.Where(v => !v.Validate(value)).Select(v => v.Message));
            RaisePropertyChanged(nameof(ValidationDescriptions));
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        public void ClearValidation()
        {
            IsValid = true;
            Errors.Clear();           
            RaisePropertyChanged(nameof(ValidationDescriptions));
        }
    }
}
