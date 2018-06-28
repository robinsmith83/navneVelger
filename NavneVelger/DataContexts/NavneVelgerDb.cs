using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace NavneVelger.DataContexts
{
    public class NavneVelgerDb : DbContext
    {
        public NavneVelgerDb(DbContextOptions<NavneVelgerDb> options) : base(options)
        {

        }

        public DbSet<Navn> Navnene { get; set; }
        public DbSet<NavnRangering> NavnRangeringer { get; set; }
    }

    public class Navn
    {
        public int Id { get; set; }
        public Gender Gender { get; set; }        
        public string Navnet { get; set; }
        public List<NavnRangering> NavnRangeringer { get; set; }
    }

    public class NavnRangering
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Navn Navn { get; set; }
        public int Rangering { get; set; }

    }

    public enum Gender
    {
        gutt,
        jente
    }
}
