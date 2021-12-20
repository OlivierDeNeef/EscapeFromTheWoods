using DataAccessLayerDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayerDB
{
    public class RecordsDataContext : DbContext
    {
        private readonly string _connstr;

        public DbSet<WoodRecord> WoodRecords { get; set; }
        public DbSet<MonkeyRecord> MonkeyRecords { get; set; }
        public DbSet<Log> Logs { get; set; }

      

        public RecordsDataContext(IConfiguration configuration)
        {
            _connstr = configuration.GetConnectionString("localHost");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connstr);
        }
    }
}