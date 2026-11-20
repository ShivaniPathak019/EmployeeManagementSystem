using EmployeeManagement.Application.Services;
using EmployeeManagement.Application.Departments.Commands.CreateDepartment;
using MediatR;

namespace EmployeeManagement.Application.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly DepartmentService _departmentService;

    public CreateDepartmentCommandHandler(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        return await _departmentService.CreateAsync(
            request.Name, request.Description,  cancellationToken);
    }
}