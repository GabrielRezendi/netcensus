using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCensus.Manager.DAL.Entities
{
    public class BasicStats
    {
        public int Id { get; set; }
        public DateTime DateTimeRegistered { get; set; }
        public string MachineNickname { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public double TotalRam { get; set; }
        public double AvaiableRam { get; set; }
        public string CpuUsage { get; set; }
        public string IpAddresses { get; set; }
        public double TotalStorage { get; set; }
        public double AvaiableStorage { get; set; }

    }
}
