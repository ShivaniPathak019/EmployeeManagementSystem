using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAt) Generate(User user, int? employeeId);
}
