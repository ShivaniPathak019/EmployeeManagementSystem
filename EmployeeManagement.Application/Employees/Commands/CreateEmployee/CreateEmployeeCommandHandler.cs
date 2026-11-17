

using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var departmentExists = await _context.Departments
            .AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);

        if (!departmentExists)
            throw new NotFoundException(nameof(DepartmentDetails), request.DepartmentId);

        var designationExists = await _context.Designations
            .AnyAsync(d => d.Id == request.DesignationId, cancellationToken);

        if (!designationExists)
            throw new NotFoundException(nameof(Designation), request.DesignationId);

        var employee = new EmployeeDetails
        {
            EmployeeCode = request.EmployeeCode,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            DateOfJoining = request.DateOfJoining,
            Salary = request.Salary,
            Address = request.Address,
            IsActive = true,
            DepartmentId = request.DepartmentId,
            DesignationId = request.DesignationId
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}