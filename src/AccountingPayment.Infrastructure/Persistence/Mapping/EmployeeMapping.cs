using AccountingPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingPayment.Infrastructure.Persistence.Mapping
{
    public class EmployeeMapping : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {

            builder.ToTable("Employee");

            builder.HasKey(c => c.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Document)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(e => e.GrossSalary)
               .IsRequired()
               .HasDefaultValueSql("0");

            builder.Property(e => e.TransportationVoucherDiscount)
                .IsRequired();

            builder.Property(e => e.DentalPlanDiscount)
                .IsRequired();

            builder.Property(e => e.HealthPlanDiscount)
                .IsRequired();

            builder.Property(e => e.AdmissionDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Deleted).IsRequired();
        }
    }
}
