using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charts.web.api.Models.Map
{
    public class CoachesMap
    {
       public CoachesMap(EntityTypeBuilder<Coaches> typeBuilder)
        {
            typeBuilder.HasNoKey();
            typeBuilder.Property(x=>x.Name).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Nation).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Discipline).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Event).HasMaxLength(30);
        } 
    }
}