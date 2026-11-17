using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand(
    int Id,
    string EmployeeCode,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    DateTime DateOfBirth,
    DateTime DateOfJoining,
    decimal Salary,
    string? Address,
    bool IsActive,
    int DepartmentId,
    int DesignationId) : IRequest;