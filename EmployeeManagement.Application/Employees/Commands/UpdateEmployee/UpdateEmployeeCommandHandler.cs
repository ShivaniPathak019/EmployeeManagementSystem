using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee is null)
            throw new NotFoundException(nameof(EmployeeDetails), request.Id);

        var departmentExists = await _context.Departments
            .AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);
        if (!departmentExists)
            throw new NotFoundException(nameof(DepartmentDetails), request.DepartmentId);

        var designationExists = await _context.Designations
            .AnyAsync(d => d.Id == request.DesignationId, cancellationToken);
        if (!designationExists)
            throw new NotFoundException(nameof(Designation), request.DesignationId);

        employee.EmployeeCode = request.EmployeeCode;
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.PhoneNumber = request.PhoneNumber;
        employee.DateOfBirth = request.DateOfBirth;
        employee.DateOfJoining = request.DateOfJoining;
        employee.Salary = request.Salary;
        employee.Address = request.Address;
        employee.IsActive = request.IsActive;
        employee.DepartmentId = request.DepartmentId;
        employee.DesignationId = request.DesignationId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}