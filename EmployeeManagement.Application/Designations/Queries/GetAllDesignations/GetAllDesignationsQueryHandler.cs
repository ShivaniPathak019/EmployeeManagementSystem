using EmployeeManagement.Application.Designations.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Designations.Queries.GetAllDesignations;

public class GetAllDesignationsQueryHandler
    : IRequestHandler<GetAllDesignationsQuery, List<DesignationDto>>
{
    private readonly DesignationService _designationService;

    public GetAllDesignationsQueryHandler(DesignationService designationService)
    {
        _designationService = designationService;
    }

    public async Task<List<DesignationDto>> Handle(
        GetAllDesignationsQuery request, CancellationToken cancellationToken)
    {
        return await _designationService.GetAllAsync(cancellationToken);
    }
}