using EmployeeManagement.Application.Common.Models;
using EmployeeManagement.Application.Employees.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQueryHandler
    : IRequestHandler<GetAllEmployeesQuery, PagedResult<EmployeeDto>>
{
    private readonly EmployeeService _employeeService;

    public GetAllEmployeesQueryHandler(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<PagedResult<EmployeeDto>> Handle(
        GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _employeeService.GetAllAsync(
            request.SearchTerm,
            request.DepartmentId,
            request.IncludeInactive,
            request.PageNumber,
            request.PageSize,
            cancellationToken);
    }
}
