using EmployeeManagement.Application.Employees.Dtos;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.GetAllEmployees;

public record GetAllEmployeesQuery : IRequest<List<EmployeeDto>>;