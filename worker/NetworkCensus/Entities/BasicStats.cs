using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCensus.Entities
{
    public class BasicStats
    {
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public double TotalRam { get; set; }
        public double AvaiableRam { get; set; }
        public List<ulong> CpuUsage { get; set; }
        public List<string> IpAddresses{ get; set; }
        public double TotalStorage { get; set; }
        public double AvaiableStorage { get; set; }
    }
}
