using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Designations.Commands.UpdateDesignation;

public class UpdateDesignationCommandHandler
    : IRequestHandler<UpdateDesignationCommand>
{
    private readonly DesignationService _designationService;

    public UpdateDesignationCommandHandler(DesignationService designationService)
    {
        _designationService = designationService;
    }

    public async Task Handle(
        UpdateDesignationCommand request, CancellationToken cancellationToken)
    {
        await _designationService.UpdateAsync(
            request.Id, request.Title, request.Description, cancellationToken);
    }
}