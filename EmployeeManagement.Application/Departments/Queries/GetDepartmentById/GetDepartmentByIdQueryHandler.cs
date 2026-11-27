using EmployeeManagement.Application.Departments.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Departments.Queries.GetDepartmentById;

public class GetDepartmentByIdQueryHandler
    : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
{
    private readonly DepartmentService _departmentService;

    public GetDepartmentByIdQueryHandler(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<DepartmentDto> Handle(
        GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _departmentService.GetByIdAsync(request.Id, cancellationToken);
    }
}