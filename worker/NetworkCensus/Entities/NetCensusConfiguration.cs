using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCensus.Worker.Entities
{
    public class NetCensusConfiguration
    {
        /// <summary>
        /// How the machine will be identified in the NetCensus Manager
        /// </summary>
        public string MachineNickname { get; set; }
        /// <summary>
        /// Amount of time to be the cadence of request to the API, in milliseconds 
        /// </summary>
        public int SendCadence { get; set; }
        /// <summary>
        /// Adddres of the Manager API
        /// </summary>
        public string NetCensusManagerAddress { get; set; }
    }
}
