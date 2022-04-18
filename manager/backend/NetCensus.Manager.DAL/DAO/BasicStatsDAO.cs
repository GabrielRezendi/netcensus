using NetCensus.Manager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCensus.Manager.DAL.DAO
{
    public class BasicStatsDAO : BaseDAO<BasicStats>
    {
        public override void Add(BasicStats entity)
        {
            entity.DateTimeRegistered = DateTime.Now;

            if (Get().Where(x => x.MachineNickname == entity.MachineNickname).Count() >= 100)
                Remove(Get().First(x => x.MachineNickname == entity.MachineNickname));

            base.Add(entity);
        }
    }
}
