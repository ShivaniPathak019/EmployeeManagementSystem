namespace EmployeeManagement.Application.Common.Interfaces;

public interface ICurrentUserService
{
    int? EmployeeId { get; }
    bool IsAdmin { get; }
    string? Email { get; }
}
