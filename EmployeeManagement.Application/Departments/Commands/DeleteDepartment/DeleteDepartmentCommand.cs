using MediatR;

namespace EmployeeManagement.Application.Departments.Commands.DeleteDepartment;

public record DeleteDepartmentCommand(int Id) : IRequest;