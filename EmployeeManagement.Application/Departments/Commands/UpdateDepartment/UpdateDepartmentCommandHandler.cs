using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (department is null)
            throw new NotFoundException(nameof(DepartmentDetails), request.Id);

        department.Name = request.Name;
        department.Description = request.Description;
        department.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);
    }
}