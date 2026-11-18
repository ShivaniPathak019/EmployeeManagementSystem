using EmployeeManagement.Application.Common.Models;
using EmployeeManagement.Application.Employees.Dtos;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.GetAllEmployees;

public record GetAllEmployeesQuery : IRequest<PagedResult<EmployeeDto>>
{
    public string? SearchTerm { get; init; }
    public int? DepartmentId { get; init; }
    public bool IncludeInactive { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}