using EmployeeManagement.Application.Departments.Dtos;
using MediatR;

namespace EmployeeManagement.Application.Departments.Queries.GetDepartmentById;

public record GetDepartmentByIdQuery(int Id) : IRequest<DepartmentDto>;