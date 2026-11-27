using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly DepartmentService _departmentService;

    public DeleteDepartmentCommandHandler(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        await _departmentService.DeleteAsync(request.Id, cancellationToken);
    }
}