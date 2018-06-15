using Microsoft.EntityFrameworkCore;
using Panini.Entities;

namespace NavneVelger.DataContexts
{
    public class BokerDb : DbContext
    {
        public BokerDb(DbContextOptions<BokerDb> options) : base(options)
        {

        }
        public DbSet<KlistremerkeBok> Boker { get; set; }
        public DbSet<Merke> Merker { get; set; }
        public DbSet<Eier> Eiere { get; set; }
        public DbSet<BokType> BokTyper { get; set; }
    }
}
