using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CompucareWard.Models
{
    public class User : BindableBase
    {
        private string _fullnameReverse;
        private int _userId;
        private int? _clinicianid;
        private int? _nurseId;
        private byte[] _thumbnail;
        private Location _location;
        private string _licenceNumber;
        private string _systemType;

        [JsonProperty(PropertyName = "systemtype")]
        public string SystemType
        {
            get => _systemType;
            set => SetProperty(ref _systemType, value);
        }

        [JsonProperty(PropertyName = "licencenumber")]
        public string LicenceNumber
        {
            get => _licenceNumber;
            set => SetProperty(ref _licenceNumber, value);
        }

        [JsonProperty(PropertyName = "userid")]
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        [JsonProperty(PropertyName = "nurseid")]
        public int? NurseId
        {
            get => _nurseId;
            set => SetProperty(ref _nurseId, value);
        }

        [JsonProperty(PropertyName = "clinicianid")]
        public int? ClinicianId
        {
            get => _clinicianid;
            set => SetProperty(ref _clinicianid, value);
        }

        [JsonProperty(PropertyName = "fullnamereverse")]
        public string FullnameReverse
        {
            get => _fullnameReverse;
            set => SetProperty(ref _fullnameReverse, value);
        }

        [JsonProperty(PropertyName = "thumbnail")]
        public byte[] Thumbnail
        {
            get => _thumbnail;
            set => SetProperty(ref _thumbnail, value);
        }

        [JsonProperty(PropertyName = "location")]
        public Location Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public string Role => NurseId.HasValue ? "Nurse" : "Clinician";

        public string Initials
        {
            get
            {
                var names = Regex.Replace(FullnameReverse, "(\".*\")|('.*')|(\\(.*\\))", "").Split(',');
                var nameReordered = names.Length > 1 ? names[1] + names[0] : names[0];

                return new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?").Replace(nameReordered.Trim(), "$1");
            }
        }
    }
}
