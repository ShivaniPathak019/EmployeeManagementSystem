using Microsoft.EntityFrameworkCore;
using System.Threading;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Department> Departments { get; }
        DbSet<Designation> Designations { get; }
        DbSet<Employee> Employees { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}