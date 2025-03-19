using App.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace App.Repositories
{
    public class StockConfiguration: IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.MalKodu).IsRequired().HasMaxLength(10);

        }
    }
}
