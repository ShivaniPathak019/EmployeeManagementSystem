using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Designations.Commands.UpdateDesignation;

public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDesignationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateDesignationCommand request, CancellationToken cancellationToken)
    {
        var designation = await _context.Designations
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (designation is null)
            throw new NotFoundException(nameof(Designation), request.Id);

        designation.Title = request.Title;
        designation.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
    }
}