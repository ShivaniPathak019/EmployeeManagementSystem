using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EmployeeManagement.Infrastructure.Persistence.Configurations;
public class DesignationConfiguration : IEntityTypeConfiguration<Designation>  
{
    public void Configure(EntityTypeBuilder<Designation> builder)
    {
        builder.ToTable("DesignationDetails");
        builder.Property(d => d.Title)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(d => d.Description)
        .HasMaxLength(250);
        builder.HasIndex(d => d.Title)
        .IsUnique();
    }
    
}