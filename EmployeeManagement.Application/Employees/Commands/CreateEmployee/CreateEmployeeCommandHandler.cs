using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly EmployeeService _employeeService;

    public CreateEmployeeCommandHandler(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<int> Handle(
        CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        return await _employeeService.CreateAsync(
            request.EmployeeCode,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.DateOfBirth,
            request.DateOfJoining,
            request.Salary,
            request.Address,
            request.DepartmentId,
            request.DesignationId,
            cancellationToken);
    }
}
