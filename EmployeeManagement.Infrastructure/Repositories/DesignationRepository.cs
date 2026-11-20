using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class DesignationRepository : IDesignationRepository
{
    private readonly ApplicationDbContext _context;

    public DesignationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Designation>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Designations.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Designation?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _context.Designations.AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<bool> TitleExistsAsync(string title, CancellationToken cancellationToken)
        => await _context.Designations.AnyAsync(d => d.Title == title, cancellationToken);

    public async Task<bool> TitleExistsForOtherAsync(string title, int excludeId, CancellationToken cancellationToken)
        => await _context.Designations
            .AnyAsync(d => d.Title == title && d.Id != excludeId, cancellationToken);

    public async Task<bool> HasEmployeesAsync(int designationId, CancellationToken cancellationToken)
        => await _context.Employees.AnyAsync(e => e.DesignationId == designationId, cancellationToken);

    public async Task<Designation> AddAsync(Designation designation, CancellationToken cancellationToken)
    {
        _context.Designations.Add(designation);
        await _context.SaveChangesAsync(cancellationToken);
        return designation;
    }

    public async Task UpdateAsync(Designation designation, CancellationToken cancellationToken)
    {
        _context.Designations.Update(designation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Designation designation, CancellationToken cancellationToken)
    {
        _context.Designations.Remove(designation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}