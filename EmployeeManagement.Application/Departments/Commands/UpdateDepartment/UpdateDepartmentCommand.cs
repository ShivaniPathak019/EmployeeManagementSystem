using MediatR;

namespace EmployeeManagement.Application.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand(int Id, string Name, string? Description, bool IsActive) : IRequest;