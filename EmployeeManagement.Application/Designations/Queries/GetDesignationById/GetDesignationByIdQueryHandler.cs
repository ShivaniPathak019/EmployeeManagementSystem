using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Designations.Dtos;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Designations.Queries.GetDesignationById;

public class GetDesignationByIdQueryHandler
    : IRequestHandler<GetDesignationByIdQuery, DesignationDto>
{
    private readonly IApplicationDbContext _context;

    public GetDesignationByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DesignationDto> Handle(
        GetDesignationByIdQuery request, CancellationToken cancellationToken)
    {
        var designation = await _context.Designations
            .AsNoTracking()
            .Where(d => d.Id == request.Id)
            .Select(d => new DesignationDto
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (designation is null)
            throw new NotFoundException(nameof(Designation), request.Id);

        return designation;
    }
}