using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly EmployeeService _employeeService;

    public UpdateEmployeeCommandHandler(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task Handle(
        UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _employeeService.UpdateAsync(
            request.Id,
            request.EmployeeCode,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.DateOfBirth,
            request.DateOfJoining,
            request.Salary,
            request.Address,
            request.IsActive,
            request.DepartmentId,
            request.DesignationId,
            cancellationToken);
    }
}
