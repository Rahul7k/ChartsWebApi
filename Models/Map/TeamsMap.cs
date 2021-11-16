using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charts.web.api.Models.Map
{
    public class TeamsMap
    {
        public TeamsMap(EntityTypeBuilder<Teams> typeBuilder)
        {
            typeBuilder.HasNoKey();
            typeBuilder.Property(x=>x.Name).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Discipline).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Nation).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Event).IsRequired().HasMaxLength(30);
        }
    }
}