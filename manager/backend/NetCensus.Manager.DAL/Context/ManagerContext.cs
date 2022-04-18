using Microsoft.EntityFrameworkCore;
using NetCensus.Manager.DAL.Entities;

namespace NetCensus.Manager.DAL.Context
{
    public class ManagerContext : DbContext
    {
        DbSet<BasicStats> BasicStats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connetionString = Environment.GetEnvironmentVariable("NETCENSUS_CONNECTIONSTRING");
            optionsBuilder.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
        }
    }
}
