
using MediatR;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Application.Designations.Dtos;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Designations.Commands.UpdateDesignationPatch;

public class UpdateDesignationPatchCommandHandler
    : IRequestHandler<UpdateDesignationPatchCommand, DesignationDto>
{
    private readonly IDesignationRepository _repo;
    private readonly ICurrentUserService _curUser;

    public UpdateDesignationPatchCommandHandler(
        IDesignationRepository repo,
        ICurrentUserService curUser)
    {
        _repo = repo;
        _curUser = curUser;
    }

    public async Task<DesignationDto> Handle(
        UpdateDesignationPatchCommand req,
        CancellationToken ct)
    {
        if (!_curUser.IsAdmin)
            throw new UnauthorizedException(
                "Only administrators can update designations.");

        var ent = await _repo.GetByIdAsync(req.Id, ct)
            ?? throw new NotFoundException(nameof(Designation), req.Id);

        if (req.Title != null)
            ent.Title = req.Title;

        if (req.Description != null)
            ent.Description = req.Description;

        await _repo.UpdateAsync(ent, ct);

        return new DesignationDto
        {
            Id = ent.Id,
            Title = ent.Title,
            Description = ent.Description
        };
    }
}
