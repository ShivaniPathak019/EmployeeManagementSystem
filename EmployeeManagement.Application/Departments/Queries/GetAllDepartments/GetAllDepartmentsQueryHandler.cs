using EmployeeManagement.Application.Departments.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Departments.Queries.GetAllDepartments;

public class GetAllDepartmentsQueryHandler
    : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDto>>
{
    private readonly DepartmentService _departmentService;

    public GetAllDepartmentsQueryHandler(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<List<DepartmentDto>> Handle(
        GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        return await _departmentService.GetAllAsync(cancellationToken);
    }
}