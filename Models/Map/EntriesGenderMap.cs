using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charts.web.api.Models.Map
{
    public class EntriesGenderMap
    {
        public EntriesGenderMap(EntityTypeBuilder<EntriesGender> typeBuilder)
        {
            typeBuilder.HasKey(x=> x.Discipline);
            typeBuilder.Property(x=>x.Discipline).IsRequired().HasMaxLength(30);
            typeBuilder.Property(x=>x.Female).IsRequired();
            typeBuilder.Property(x=>x.Male).IsRequired();
            typeBuilder.Property(x=>x.Total).IsRequired();
        }
    }
}