using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Application.Common.Models;
using EmployeeManagement.Application.Employees.Dtos;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICurrentUserService _currentUserService;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        ICurrentUserService currentUserService)
    {
        _employeeRepository = employeeRepository;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<EmployeeDto>> GetAllAsync(
        string? searchTerm,
        int? departmentId,
        bool includeInactive,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        int? employeeId = null;

        if (!_currentUserService.IsAdmin)
        {
            employeeId = _currentUserService.EmployeeId;

            if (employeeId is null)
            {
                return new PagedResult<EmployeeDto>
                {
                    Items = new List<EmployeeDto>(),
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = 0
                };
            }
        }

        var result = await _employeeRepository.GetAllAsync(
            employeeId,
            searchTerm,
            departmentId,
            includeInactive,
            pageNumber,
            pageSize,
            cancellationToken);

        return new PagedResult<EmployeeDto>
        {
            Items = result.Items.Select(ToDto).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<EmployeeDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAdmin &&
            (_currentUserService.EmployeeId is null || _currentUserService.EmployeeId != id))
            throw new UnauthorizedException("You can only view your own information.");

        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(EmployeeDetails), id);

        return ToDto(employee);
    }

    public async Task<int> CreateAsync(
        string employeeCode,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        DateTime dateOfBirth,
        DateTime dateOfJoining,
        decimal salary,
        string? address,
        int departmentId,
        int designationId,
        CancellationToken cancellationToken)
    {
        if (!await _employeeRepository.DepartmentExistsAsync(departmentId, cancellationToken))
            throw new NotFoundException(nameof(DepartmentDetails), departmentId);

        if (!await _employeeRepository.DesignationExistsAsync(designationId, cancellationToken))
            throw new NotFoundException(nameof(Designation), designationId);

        var employee = new EmployeeDetails
        {
            EmployeeCode = employeeCode,
            FirstName = firstName,
            LastName = lastName,
            Email = email.Trim().ToLowerInvariant(),
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            DateOfJoining = dateOfJoining,
            Salary = salary,
            Address = address,
            IsActive = true,
            DepartmentId = departmentId,
            DesignationId = designationId
        };

        var created = await _employeeRepository.AddAsync(employee, cancellationToken);
        return created.Id;
    }

    public async Task UpdateAsync(
        int id,
        string employeeCode,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        DateTime dateOfBirth,
        DateTime dateOfJoining,
        decimal salary,
        string? address,
        bool isActive,
        int departmentId,
        int designationId,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdForWriteAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(EmployeeDetails), id);

        if (!await _employeeRepository.DepartmentExistsAsync(departmentId, cancellationToken))
            throw new NotFoundException(nameof(DepartmentDetails), departmentId);

        if (!await _employeeRepository.DesignationExistsAsync(designationId, cancellationToken))
            throw new NotFoundException(nameof(Designation), designationId);

        employee.EmployeeCode = employeeCode;
        employee.FirstName = firstName;
        employee.LastName = lastName;
        employee.Email = email.Trim().ToLowerInvariant();
        employee.PhoneNumber = phoneNumber;
        employee.DateOfBirth = dateOfBirth;
        employee.DateOfJoining = dateOfJoining;
        employee.Salary = salary;
        employee.Address = address;
        employee.IsActive = isActive;
        employee.DepartmentId = departmentId;
        employee.DesignationId = designationId;

        await _employeeRepository.UpdateAsync(employee, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdForWriteAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(EmployeeDetails), id);

        await _employeeRepository.DeleteAsync(employee, cancellationToken);
    }

    private static EmployeeDto ToDto(EmployeeDetails e) => new()
    {
        Id = e.Id,
        EmployeeCode = e.EmployeeCode,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        PhoneNumber = e.PhoneNumber,
        DateOfBirth = e.DateOfBirth,
        DateOfJoining = e.DateOfJoining,
        Salary = e.Salary,
        Address = e.Address,
        IsActive = e.IsActive,
        DepartmentId = e.DepartmentId,
        DepartmentName = e.Department.Name,
        DesignationId = e.DesignationId,
        DesignationTitle = e.Designation.Title
    };
}
