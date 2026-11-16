using Microsoft.EntityFrameworkCore;
using System.Threading;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<DepartmentDetails> Departments { get; }
        DbSet<Designation> Designations { get; }
        DbSet<EmployeeDetails> Employees { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}