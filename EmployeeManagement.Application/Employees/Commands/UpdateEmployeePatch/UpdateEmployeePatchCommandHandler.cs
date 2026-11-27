
using MediatR;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Application.Employees.Dtos;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployeePatch;

public class UpdateEmployeePatchCommandHandler
    : IRequestHandler<UpdateEmployeePatchCommand, EmployeeDto>
{
    private readonly IEmployeeRepository _repo;
    private readonly ICurrentUserService _curUser;

    public UpdateEmployeePatchCommandHandler(
        IEmployeeRepository repo,
        ICurrentUserService curUser)
    {
        _repo = repo;
        _curUser = curUser;
    }

    public async Task<EmployeeDto> Handle(
        UpdateEmployeePatchCommand req,
        CancellationToken ct)
    {
        if (!_curUser.IsAdmin &&
            (_curUser.EmployeeId is null ||
             _curUser.EmployeeId != req.Id))
        {
            throw new UnauthorizedException(
                "You can only view your own information.");
        }

        var ent = await _repo.GetByIdForWriteAsync(req.Id, ct)
            ?? throw new NotFoundException(nameof(EmployeeDetails), req.Id);

        if (req.EmployeeCode != null) ent.EmployeeCode = req.EmployeeCode;
        if (req.FirstName != null) ent.FirstName = req.FirstName;
        if (req.LastName != null) ent.LastName = req.LastName;
        if (req.Email != null) ent.Email = req.Email.Trim().ToLowerInvariant();
        if (req.PhoneNumber != null) ent.PhoneNumber = req.PhoneNumber;
        if (req.IsActive.HasValue) ent.IsActive = req.IsActive.Value;
        if (req.Salary.HasValue) ent.Salary = req.Salary.Value;
        if (req.DateOfBirth.HasValue) ent.DateOfBirth = req.DateOfBirth.Value;
        if (req.DateOfJoining.HasValue) ent.DateOfJoining = req.DateOfJoining.Value;
        if (req.Address != null) ent.Address = req.Address;
        if (req.DepartmentId.HasValue) ent.DepartmentId = req.DepartmentId.Value;
        if (req.DesignationId.HasValue) ent.DesignationId = req.DesignationId.Value;

        await _repo.UpdateAsync(ent, ct);

        return new EmployeeDto
        {
            Id = ent.Id,
            EmployeeCode = ent.EmployeeCode,
            FirstName = ent.FirstName,
            LastName = ent.LastName,
            Email = ent.Email,
            PhoneNumber = ent.PhoneNumber,
            DateOfBirth = ent.DateOfBirth,
            DateOfJoining = ent.DateOfJoining,
            Salary = ent.Salary,
            Address = ent.Address,
            IsActive = ent.IsActive,
            DepartmentId = ent.DepartmentId,
            DepartmentName = ent.Department?.Name ?? "",
            DesignationId = ent.DesignationId,
            DesignationTitle = ent.Designation?.Title ?? ""
        };
    }
}
