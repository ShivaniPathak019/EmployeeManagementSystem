using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Designations.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Designations.Queries.GetAllDesignations;

public class GetAllDesignationsQueryHandler
    : IRequestHandler<GetAllDesignationsQuery, List<DesignationDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllDesignationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DesignationDto>> Handle(
        GetAllDesignationsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Designations
            .AsNoTracking()
            .Select(d => new DesignationDto
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description
            })
            .ToListAsync(cancellationToken);
    }
}