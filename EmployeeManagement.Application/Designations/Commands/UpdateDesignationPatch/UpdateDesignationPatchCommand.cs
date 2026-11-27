using MediatR;

using EmployeeManagement.Application.Designations.Dtos;

namespace EmployeeManagement.Application.Designations.Commands.UpdateDesignationPatch;

public record UpdateDesignationPatchCommand : IRequest<DesignationDto>
{
    // Set from the route id in the controller; the JSON body only carries the fields to update.
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}
