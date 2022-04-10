using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpStart.Domain.ViewModels.Location
{
    public class AddressResultVM
    {
        public string MatchedAddress { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string StreetName { get; set; }
        public string SuffixType { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
