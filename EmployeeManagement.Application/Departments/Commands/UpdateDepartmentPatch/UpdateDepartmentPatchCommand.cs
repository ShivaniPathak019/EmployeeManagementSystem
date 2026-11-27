using MediatR;

using EmployeeManagement.Application.Departments.Dtos;

namespace EmployeeManagement.Application.Departments.Commands.UpdateDepartmentPatch;

public record UpdateDepartmentPatchCommand : IRequest<DepartmentDto>
{
    // Set from the route id in the controller; the JSON body only carries the fields to update.
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}
