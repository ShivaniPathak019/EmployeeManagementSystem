using MediatR;

namespace EmployeeManagement.Application.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand(string Name, string? Description) : IRequest<int>;