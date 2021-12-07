using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charts.web.api.Models.Map
{
    public class TeamsMap
    {
        public TeamsMap(EntityTypeBuilder<Teams> typeBuilder)
        {
            typeBuilder.HasKey(x=>x.SNo);
            typeBuilder.Property(x=>x.Name).IsRequired();
            typeBuilder.Property(x=>x.Discipline).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Nation).IsRequired();
            typeBuilder.Property(x=>x.Event).IsRequired();
        }
    }
}