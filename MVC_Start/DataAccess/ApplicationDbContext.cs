using Microsoft.EntityFrameworkCore;
using MVC_Start.Models;

namespace MVC_Start.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    //public DbSet<School> Schoolsdata { get; set; }
    //    //public DbSet<Quote> Quotes { get; set; }

        public DbSet<SchoolData> Schools { get; set; }
        public DbSet<ProgramData> Programs { get; set; }

    }
}