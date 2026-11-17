using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;

namespace EmployeeManagement.Application.Designations.Commands.CreateDesignation;

public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateDesignationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateDesignationCommand request, CancellationToken cancellationToken)
    {
        var designation = new Designation
        {
            Title = request.Title,
            Description = request.Description
        };

        _context.Designations.Add(designation);
        await _context.SaveChangesAsync(cancellationToken);

        return designation.Id;
    }
}