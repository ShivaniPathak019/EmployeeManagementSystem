using EmployeeManagement.Application.Employees.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly EmployeeService _employeeService;

    public GetEmployeeByIdQueryHandler(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<EmployeeDto> Handle(
        GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _employeeService.GetByIdAsync(request.Id, cancellationToken);
    }
}
