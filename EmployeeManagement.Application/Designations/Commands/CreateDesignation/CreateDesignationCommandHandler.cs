using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Designations.Commands.CreateDesignation;

public class CreateDesignationCommandHandler
    : IRequestHandler<CreateDesignationCommand, int>
{
    private readonly DesignationService _designationService;

    public CreateDesignationCommandHandler(DesignationService designationService)
    {
        _designationService = designationService;
    }

    public async Task<int> Handle(
        CreateDesignationCommand request, CancellationToken cancellationToken)
    {
        return await _designationService.CreateAsync(
            request.Title, request.Description, cancellationToken);
    }
}