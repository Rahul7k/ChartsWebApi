using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charts.web.api.Models.Map
{
    public class AthletesMap
    {
        public AthletesMap(EntityTypeBuilder<Athletes> typeBuilder)
        {
            typeBuilder.HasKey(x=>x.SNo);
            typeBuilder.Property(x=>x.Name).IsRequired();
            typeBuilder.Property(x=>x.Nation).IsRequired();
            typeBuilder.Property(x=>x.Discipline).IsRequired().HasMaxLength(30);
        }
    }
}