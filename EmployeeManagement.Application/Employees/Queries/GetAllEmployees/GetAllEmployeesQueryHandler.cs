using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Employees.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQueryHandler
    : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllEmployeesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDto>> Handle(
        GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeDto
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
            })
            .ToListAsync(cancellationToken);
    }
}