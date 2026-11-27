using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentDetails>
{
    public void Configure(EntityTypeBuilder<DepartmentDetails> builder)
    {
        builder.ToTable("DepartmentDetails");

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Description)
            .HasMaxLength(500);

        builder.HasIndex(d => d.Name)
            .IsUnique();
    }
}