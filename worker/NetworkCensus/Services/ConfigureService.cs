using NetCensus.Worker.Entities;
using Newtonsoft.Json;
using Pastel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCensus.Worker.Services
{
    public class ConfigureService
    {
        const string configurationFile = "netcensus-worker.settings";

        public static NetCensusConfiguration Get()
        {
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + $"\\{configurationFile}")) {
                return JsonConvert.DeserializeObject<NetCensusConfiguration>(reader.ReadToEnd());
            }
        }

        public void Check()
        {
            if (!File.Exists(Environment.CurrentDirectory + $"\\{configurationFile}"))
            {
                Console.WriteLine("The configure file was not found, starting setup...".Pastel(Color.Yellow));
                Setup();
            }
            else {
                Console.WriteLine("* Configuration file " + "found.".Pastel(Color.Green));
            }
        }

        public void Setup()
        {
            var path = Environment.CurrentDirectory + $"\\{configurationFile}";



            NetCensusConfiguration configuration = new NetCensusConfiguration();

            Console.Write("> NetCensus Manager Address: ");
            configuration.NetCensusManagerAddress = Console.ReadLine();

            if (configuration.NetCensusManagerAddress.Last() != '/')
                configuration.NetCensusManagerAddress += "/";

            Console.Write("> Name of the machine (it will be indetified as so in the NetCensus Manager): ");
            configuration.MachineNickname = Console.ReadLine();

            Console.Write("> Sending cadence (in millisencods): ");
            configuration.SendCadence = Convert.ToInt32(Console.ReadLine());


            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(JsonConvert.SerializeObject(configuration));
                
                Console.WriteLine("Configuration file created.".Pastel(Color.Green));
            }
        }
    }
}
