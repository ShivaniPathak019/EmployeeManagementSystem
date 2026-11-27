using EmployeeManagement.Application.Common.Models;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<PagedResult<EmployeeDetails>> GetAllAsync(
        int? employeeId,
        string? searchTerm,
        int? departmentId,
        bool includeInactive,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
    Task<EmployeeDetails?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<EmployeeDetails?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<EmployeeDetails?> GetByIdForWriteAsync(int id, CancellationToken cancellationToken);
    Task<bool> DepartmentExistsAsync(int departmentId, CancellationToken cancellationToken);
    Task<bool> DesignationExistsAsync(int designationId, CancellationToken cancellationToken);
    Task<EmployeeDetails> AddAsync(EmployeeDetails employee, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeDetails employee, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeDetails employee, CancellationToken cancellationToken);
}
