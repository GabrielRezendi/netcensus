using Microsoft.AspNetCore.Mvc;
using NetCensus.Manager.DAL.DAO;
using NetCensus.Manager.DTO;
using Newtonsoft.Json;

namespace NetCensus.Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasicStatsController : ControllerBase
    {
        private readonly ILogger<BasicStatsController> _logger;
        private readonly IBaseDAO<DAL.Entities.BasicStats> _dao;

        public BasicStatsController(ILogger<BasicStatsController> logger, IBaseDAO<DAL.Entities.BasicStats> dao)
        {
            _logger = logger;
            _dao = dao;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_dao.Get());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(BasicStatusRequestDTO dto)
        {
            try
            {
                var basicStats = new DAL.Entities.BasicStats();
                basicStats.SerialNumber = dto.SerialNumber;
                basicStats.MachineNickname = dto.MachineNickname;
                basicStats.Manufacturer = dto.Manufacturer;
                basicStats.AvaiableStorage = dto.AvaiableStorage;
                basicStats.TotalStorage = dto.TotalStorage;
                basicStats.TotalRam = dto.TotalRam;
                basicStats.AvaiableRam = dto.AvaiableRam;
                basicStats.IpAddresses = JsonConvert.SerializeObject(dto.IpAddresses);
                basicStats.CpuUsage = JsonConvert.SerializeObject(dto.CpuUsage);

                _dao.Add(basicStats);

                return Ok(basicStats);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}