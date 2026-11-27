using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Designations.Commands.DeleteDesignation;

public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand>
{
    private readonly DesignationService _designationService;

    public DeleteDesignationCommandHandler(DesignationService designationService)
    {
        _designationService = designationService;
    }

    public async Task Handle(
        DeleteDesignationCommand request, CancellationToken cancellationToken)
    {
        await _designationService.DeleteAsync(request.Id, cancellationToken);
    }
}