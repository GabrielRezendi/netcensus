namespace NetCensus.Manager.DTO
{
    public class BasicStatusRequestDTO
    {
        public string MachineNickname { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public double TotalRam { get; set; }
        public double AvaiableRam { get; set; }
        public ulong[] CpuUsage { get; set; }
        public string[] IpAddresses { get; set; }
        public double TotalStorage { get; set; }
        public double AvaiableStorage { get; set; }

    }
}