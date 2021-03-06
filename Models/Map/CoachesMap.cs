using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charts.web.api.Models.Map
{
    public class CoachesMap
    {
       public CoachesMap(EntityTypeBuilder<Coaches> typeBuilder)
        {
            typeBuilder.HasKey(x=>x.SNo);
            typeBuilder.Property(x=>x.Name).IsRequired();
            typeBuilder.Property(x=>x.Nation).IsRequired();
            typeBuilder.Property(x=>x.Discipline).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Event).HasMaxLength(30);
        } 
    }
}