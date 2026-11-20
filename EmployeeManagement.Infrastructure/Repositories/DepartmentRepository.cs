using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentDetails>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<DepartmentDetails?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AnyAsync(d => d.Name == name, cancellationToken);
    }

    public async Task<DepartmentDetails> AddAsync(DepartmentDetails department, CancellationToken cancellationToken)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);
        return department;
    }

    public async Task<bool> NameExistsForOtherAsync(string name, int excludeId, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AnyAsync(d => d.Name == name && d.Id != excludeId, cancellationToken);
    }

    public async Task<bool> HasEmployeesAsync(int departmentId, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AnyAsync(e => e.DepartmentId == departmentId, cancellationToken);
    }

    public async Task UpdateAsync(DepartmentDetails department, CancellationToken cancellationToken)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DepartmentDetails department, CancellationToken cancellationToken)
    {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync(cancellationToken);
    }
}