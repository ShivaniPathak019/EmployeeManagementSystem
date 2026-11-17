using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Designations.Commands.DeleteDesignation;

public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDesignationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteDesignationCommand request, CancellationToken cancellationToken)
    {
        var designation = await _context.Designations
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (designation is null)
            throw new NotFoundException(nameof(Designation), request.Id);

        _context.Designations.Remove(designation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}