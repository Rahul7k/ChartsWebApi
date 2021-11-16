using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charts.web.api.Models;
using charts.web.api.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace charts.web.api.Repository
{
    public class ChartsDbContext: DbContext
    {
        public ChartsDbContext(DbContextOptions contextOptions): base(contextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AthletesMap(modelBuilder.Entity<Athletes>());
            new CoachesMap(modelBuilder.Entity<Coaches>());
            new EntriesGenderMap(modelBuilder.Entity<EntriesGender>());
            new MedalsMap(modelBuilder.Entity<Medals>());
            new TeamsMap(modelBuilder.Entity<Teams>());
        }
    }
}