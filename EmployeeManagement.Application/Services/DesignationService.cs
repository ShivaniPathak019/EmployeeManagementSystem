using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Application.Designations.Dtos;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services;

public class DesignationService
{
    private readonly IDesignationRepository _repository;

    public DesignationService(IDesignationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DesignationDto>> GetAllAsync(CancellationToken ct)
    {
        var items = await _repository.GetAllAsync(ct);
        return items.Select(ToDto).ToList();
    }

    public async Task<DesignationDto> GetByIdAsync(int id, CancellationToken ct)
    {
        var item = await _repository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Designation), id);
        return ToDto(item);
    }

    public async Task<int> CreateAsync(string title, string? description, CancellationToken ct)
    {
        if (await _repository.TitleExistsAsync(title, ct))
            throw new ConflictException($"Designation '{title}' already exists.");

        var created = await _repository.AddAsync(
            new Designation { Title = title, Description = description }, ct);
        return created.Id;
    }

    public async Task UpdateAsync(int id, string title, string? description, CancellationToken ct)
    {
        var item = await _repository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Designation), id);

        if (await _repository.TitleExistsForOtherAsync(title, id, ct))
            throw new ConflictException($"Designation '{title}' already exists.");

        item.Title = title;
        item.Description = description;
        await _repository.UpdateAsync(item, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var item = await _repository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Designation), id);

        if (await _repository.HasEmployeesAsync(id, ct))
            throw new ConflictException("Cannot delete a designation that still has employees.");

        await _repository.DeleteAsync(item, ct);
    }

    private static DesignationDto ToDto(Designation d) => new()
    {
        Id = d.Id,
        Title = d.Title,
        Description = d.Description
    };
}