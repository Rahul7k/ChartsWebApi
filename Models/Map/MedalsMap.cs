using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charts.web.api.Models.Map
{
    public class MedalsMap
    {
        public MedalsMap(EntityTypeBuilder<Medals> typeBuilder)
        {
            typeBuilder.HasKey(x=>x.Nation);
            typeBuilder.Property(x=>x.Gold).IsRequired();
            typeBuilder.Property(x=>x.Silver).IsRequired();
            typeBuilder.Property(x=>x.Bronze).IsRequired();
            typeBuilder.Property(x=>x.Total).IsRequired();
            typeBuilder.Property(x=>x.RankByTotal).IsRequired();
        }
    }
}