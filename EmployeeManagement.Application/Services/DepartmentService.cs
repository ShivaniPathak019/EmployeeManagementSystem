using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Application.Departments.Dtos;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Domain.Entities;
namespace EmployeeManagement.Application.Services;

public class DepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);

        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            IsActive = d.IsActive
        }).ToList();
    }


    public async Task<DepartmentDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);

        if (department is null)
            throw new NotFoundException(nameof(DepartmentDetails), id);

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            IsActive = department.IsActive
        };
    }

    public async Task<int> CreateAsync(
    string name, string? description, CancellationToken cancellationToken)
    {
        if (await _departmentRepository.NameExistsAsync(name, cancellationToken))
            throw new ConflictException($"Department '{name}' already exists.");

        var department = new DepartmentDetails
        {
            Name = name,
            Description = description,
            IsActive = true
        };

        var created = await _departmentRepository.AddAsync(department, cancellationToken);
        return created.Id;
    }


    public async Task UpdateAsync(
    int id, string name, string? description, bool isActive, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
        if (department is null)
            throw new NotFoundException(nameof(DepartmentDetails), id);

        if (await _departmentRepository.NameExistsForOtherAsync(name, id, cancellationToken))
            throw new ConflictException($"Department '{name}' already exists.");

        department.Name = name;
        department.Description = description;
        department.IsActive = isActive;

        await _departmentRepository.UpdateAsync(department, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
        if (department is null)
            throw new NotFoundException(nameof(DepartmentDetails), id);

        if (await _departmentRepository.HasEmployeesAsync(id, cancellationToken))
            throw new ConflictException("Cannot delete a department that still has employees.");

        await _departmentRepository.DeleteAsync(department, cancellationToken);
    }
}