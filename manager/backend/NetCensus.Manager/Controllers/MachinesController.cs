using Microsoft.AspNetCore.Mvc;
using NetCensus.Manager.DAL.DAO;
using NetCensus.Manager.DTO;
using Newtonsoft.Json;

namespace NetCensus.Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly ILogger<MachinesController> _logger;
        private readonly IBaseDAO<DAL.Entities.BasicStats> _dao;

        public MachinesController(ILogger<MachinesController> logger, IBaseDAO<DAL.Entities.BasicStats> dao)
        {
            _logger = logger;
            _dao = dao;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "search[]")] string[]? search)
        {
            try
            {
                var machines = new List<string>();

                if (search.Count() == 0)
                    machines = _dao.Get().Select(x => x.MachineNickname).Distinct().ToList();
                else
                    machines = _dao.Get().Where(x => search.Contains(x.MachineNickname)).Select(x => x.MachineNickname).Distinct().ToList();


                List<object> result = new List<object>();
                Dictionary<string, List<DAL.Entities.BasicStats>> resultDictionary = new Dictionary<string, List<DAL.Entities.BasicStats>>();

                foreach (var machine in machines)
                {
                    result.Add(new
                    {
                        machine = machine,
                        stats = _dao.Get().Where(x => x.MachineNickname == machine).Take(100).ToList()
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}