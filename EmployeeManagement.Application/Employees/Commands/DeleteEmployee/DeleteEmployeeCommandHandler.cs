using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly EmployeeService _employeeService;

    public DeleteEmployeeCommandHandler(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task Handle(
        DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _employeeService.DeleteAsync(request.Id, cancellationToken);
    }
}
