using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<List<DepartmentDetails>> GetAllAsync(CancellationToken cancellationToken);
    Task<DepartmentDetails?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken);
    Task<DepartmentDetails> AddAsync(DepartmentDetails department, CancellationToken cancellationToken);
    Task<bool> NameExistsForOtherAsync(string name, int excludeId, CancellationToken cancellationToken);
    Task<bool> HasEmployeesAsync(int departmentId, CancellationToken cancellationToken);
    Task UpdateAsync(DepartmentDetails department, CancellationToken cancellationToken);
    Task DeleteAsync(DepartmentDetails department, CancellationToken cancellationToken);
}