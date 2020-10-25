using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WTProgrammingTestShihab.Models
{
    public class Datum
    {
        public int value { get; set; }
        public string location { get; set; }
    }

    public class MyArray
    {
        public int value { get; set; }
        public string characters { get; set; }
        public List<Datum> data { get; set; }
    }

}