using AccountingPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingPayment.Infrastructure.Persistence.Mapping
{
    public class SectorMapping : IEntityTypeConfiguration<SectorEntity>
    {
        public void Configure(EntityTypeBuilder<SectorEntity> builder)
        {
            builder.ToTable("Sector");
            builder.HasKey(c => c.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasMaxLength(50);

            builder.Property(e => e.Deleted).IsRequired();
        }
    }
}
