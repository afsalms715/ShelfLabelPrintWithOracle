using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelfLabelPrintAPI.Models
{
    public class JsonData
    {
        public int id { get; set; }
        public string barcode { get; set; }
        public string suDesc { get; set; }
        public string sudescAr { get; set; }
        public string rsp { get; set; }
        public string rspAr { get; set; }
    }
}
