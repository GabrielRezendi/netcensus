using Hardware.Info;
using Microsoft.Extensions.Configuration;
using NetCensus.Entities;
using Newtonsoft.Json;
using NetCensus.Worker.Entities;
using NetCensus.Worker.Services;
using System.Text;
using System.Net.Http.Headers;
using Pastel;
using System.Drawing;

namespace NetCensus.Worker
{
    public class Worker
    {
        static readonly IHardwareInfo hardwareInfo = new HardwareInfo();
        private HttpClient httpClient = new HttpClient();
        private readonly IConfiguration _appConfiguration;
        private readonly NetCensusConfiguration netCensusConfiguration;

        public Worker(IConfiguration configuration)
        {
            netCensusConfiguration = ConfigureService.Get();
            _appConfiguration = configuration;
        }

        public void DoWork()
        {
            try
            {
                while (true)
                {
                    SendBasicStats();
                    Thread.Sleep(netCensusConfiguration.SendCadence);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + $": Failed to send basic stats to Manager. ERR: {ex.Message}".Pastel(Color.Red));
            }

        }

        public void SendBasicStats()
        {
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(GetBasicStats())));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var request = httpClient.PostAsync(netCensusConfiguration.NetCensusManagerAddress + "basicstats", byteContent);
            if (request.Result.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now.ToString() + ": Sent basic stats to Manager.".Pastel(Color.Green));
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString() + $": Failed to send basic stats to Manager. ERR: {request.Result.Content.ReadAsStringAsync().Result}".Pastel(Color.Red));
            }
        }

        public BasicStats GetBasicStats()
        {
            hardwareInfo.RefreshDriveList();
            double totalStorage = 0;
            double avaiableStorage = 0;
            foreach (var drive in hardwareInfo.DriveList)
            {
                foreach (var partition in drive.PartitionList)
                {
                    foreach (var volume in partition.VolumeList)
                    {
                        totalStorage += Convert.ToDouble(volume.Size) / (1024 * 1024 * 1024);
                        avaiableStorage += Convert.ToDouble(volume.FreeSpace) / (1024 * 1024 * 1024);
                    }
                }
            }

            hardwareInfo.RefreshMemoryStatus();
            hardwareInfo.RefreshBIOSList();
            var basicStats = new BasicStats()
            {
                MachineNickname = netCensusConfiguration.MachineNickname,
                TotalRam = Convert.ToDouble(hardwareInfo.MemoryStatus.TotalPhysical) / (1024 * 1024 * 1024),
                AvaiableRam = Convert.ToDouble(hardwareInfo.MemoryStatus.AvailablePhysical) / (1024 * 1024 * 1024),
                CpuUsage = new List<ulong>(),
                IpAddresses = new List<string>(),
                Manufacturer = hardwareInfo.BiosList.First().Manufacturer,
                SerialNumber = hardwareInfo.BiosList.First().SerialNumber,
                TotalStorage = totalStorage,
                AvaiableStorage = avaiableStorage,
            };

            hardwareInfo.RefreshNetworkAdapterList();

            foreach (var address in HardwareInfo.GetLocalIPv4Addresses())
                basicStats.IpAddresses.Add(address.ToString());

            //The CPU info is taken last so it reflects the latest percentage of CPU usage.
            hardwareInfo.RefreshCPUList();
            foreach (var cpu in hardwareInfo.CpuList)
                basicStats.CpuUsage.Add(cpu.PercentProcessorTime);

            return basicStats;
        }
    }
}
