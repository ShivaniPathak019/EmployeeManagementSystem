using EmployeeManagement.Application.Departments.Dtos;
using MediatR;

namespace EmployeeManagement.Application.Departments.Queries.GetAllDepartments;

public record GetAllDepartmentsQuery : IRequest<List<DepartmentDto>>;