using EmployeeManagement.Application.Designations.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Designations.Queries.GetDesignationById;

public class GetDesignationByIdQueryHandler
    : IRequestHandler<GetDesignationByIdQuery, DesignationDto>
{
    private readonly DesignationService _designationService;

    public GetDesignationByIdQueryHandler(DesignationService designationService)
    {
        _designationService = designationService;
    }

    public async Task<DesignationDto> Handle(
        GetDesignationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _designationService.GetByIdAsync(request.Id, cancellationToken);
    }
}