using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpStart.Domain.Models.Geocoding
{

    public class LocationResult
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public Input input { get; set; }
        public Addressmatch[] addressMatches { get; set; }
    }

    public class Input
    {
        public Benchmark benchmark { get; set; }
        public Address address { get; set; }
    }

    public class Benchmark
    {
        public string id { get; set; }
        public string benchmarkName { get; set; }
        public string benchmarkDescription { get; set; }
        public bool isDefault { get; set; }
    }

    public class Address
    {
        public string address { get; set; }
    }

    public class Addressmatch
    {
        public string matchedAddress { get; set; }
        public Coordinates coordinates { get; set; }
        public Tigerline tigerLine { get; set; }
        public Addresscomponents addressComponents { get; set; }
    }

    public class Coordinates
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class Tigerline
    {
        public string tigerLineId { get; set; }
        public string side { get; set; }
    }

    public class Addresscomponents
    {
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string preQualifier { get; set; }
        public string preDirection { get; set; }
        public string preType { get; set; }
        public string streetName { get; set; }
        public string suffixType { get; set; }
        public string suffixDirection { get; set; }
        public string suffixQualifier { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }

}
