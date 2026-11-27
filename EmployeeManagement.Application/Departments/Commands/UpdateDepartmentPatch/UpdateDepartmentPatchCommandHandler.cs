
using MediatR;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Application.Departments.Dtos;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Departments.Commands.UpdateDepartmentPatch;

public class UpdateDepartmentPatchCommandHandler
    : IRequestHandler<UpdateDepartmentPatchCommand, DepartmentDto>
{
    private readonly IDepartmentRepository _repo;
    private readonly ICurrentUserService _curUser;

    public UpdateDepartmentPatchCommandHandler(
        IDepartmentRepository repo,
        ICurrentUserService curUser)
    {
        _repo = repo;
        _curUser = curUser;
    }

    public async Task<DepartmentDto> Handle(
        UpdateDepartmentPatchCommand req,
        CancellationToken ct)
    {
        if (!_curUser.IsAdmin)
            throw new UnauthorizedException(
                "Only administrators can update departments.");

        var ent = await _repo.GetByIdAsync(req.Id, ct)
            ?? throw new NotFoundException(nameof(DepartmentDetails), req.Id);

        if (req.Name != null)
            ent.Name = req.Name;

        if (req.Description != null)
            ent.Description = req.Description;

        if (req.IsActive.HasValue)
            ent.IsActive = req.IsActive.Value;

        await _repo.UpdateAsync(ent, ct);

        return new DepartmentDto
        {
            Id = ent.Id,
            Name = ent.Name,
            Description = ent.Description,
            IsActive = ent.IsActive
        };
    }
}
