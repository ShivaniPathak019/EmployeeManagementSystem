using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Interfaces.Repositories;

public interface IDesignationRepository
{
    Task<List<Designation>> GetAllAsync(CancellationToken cancellationToken);
    Task<Designation?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> TitleExistsAsync(string title, CancellationToken cancellationToken);
    Task<bool> TitleExistsForOtherAsync(string title, int excludeId, CancellationToken cancellationToken);
    Task<bool> HasEmployeesAsync(int designationId, CancellationToken cancellationToken);
    Task<Designation> AddAsync(Designation designation, CancellationToken cancellationToken);
    Task UpdateAsync(Designation designation, CancellationToken cancellationToken);
    Task DeleteAsync(Designation designation, CancellationToken cancellationToken);
}