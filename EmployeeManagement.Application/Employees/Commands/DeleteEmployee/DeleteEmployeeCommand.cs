using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest;