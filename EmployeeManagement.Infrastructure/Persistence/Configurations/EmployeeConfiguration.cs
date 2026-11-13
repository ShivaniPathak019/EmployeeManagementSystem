using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        // Employee Code: required, max 20, unique
        builder.Property(e => e.EmployeeCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(e => e.EmployeeCode)
            .IsUnique();

        // Names: required, max 50
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        // Email: required, max 100, unique
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e => e.Email)
            .IsUnique();

        // Optional fields with max lengths
        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(e => e.Address)
            .HasMaxLength(250);

        // Salary: precision 18, scale 2
        builder.Property(e => e.Salary)
            .HasPrecision(18, 2); //(totaldigits, no.of digits after decimal )

        // Employee -> Department (many-to-one), Restrict delete
        builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Employee -> Designation (many-to-one), Restrict delete
        builder.HasOne(e => e.Designation)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DesignationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}