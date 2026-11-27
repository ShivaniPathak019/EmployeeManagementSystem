using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly DepartmentService _departmentService;

    public UpdateDepartmentCommandHandler(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        await _departmentService.UpdateAsync(
            request.Id, request.Name, request.Description, request.IsActive, cancellationToken);
    }
}