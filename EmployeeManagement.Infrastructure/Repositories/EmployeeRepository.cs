using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Application.Common.Models;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<EmployeeDetails>> GetAllAsync(
        int? employeeId,
        string? searchTerm,
        int? departmentId,
        bool includeInactive,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<EmployeeDetails> query = _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .Include(e => e.Designation);

        if (employeeId.HasValue)
            query = query.Where(e => e.Id == employeeId.Value);

        if (!includeInactive)
            query = query.Where(e => e.IsActive);

        if (departmentId.HasValue)
            query = query.Where(e => e.DepartmentId == departmentId.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(e =>
                e.FirstName.Contains(term) ||
                e.LastName.Contains(term) ||
                e.Email.Contains(term) ||
                e.EmployeeCode.Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(e => e.Id)
            .ThenBy(e => e.FirstName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<EmployeeDetails>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<EmployeeDetails?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .Include(e => e.Designation)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<EmployeeDetails?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);

    public async Task<EmployeeDetails?> GetByIdForWriteAsync(int id, CancellationToken cancellationToken)
        => await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<bool> DepartmentExistsAsync(int departmentId, CancellationToken cancellationToken)
        => await _context.Departments
            .AnyAsync(d => d.Id == departmentId, cancellationToken);

    public async Task<bool> DesignationExistsAsync(int designationId, CancellationToken cancellationToken)
        => await _context.Designations
            .AnyAsync(d => d.Id == designationId, cancellationToken);

    public async Task<EmployeeDetails> AddAsync(EmployeeDetails employee, CancellationToken cancellationToken)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return employee;
    }

    public async Task UpdateAsync(EmployeeDetails employee, CancellationToken cancellationToken)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmployeeDetails employee, CancellationToken cancellationToken)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
