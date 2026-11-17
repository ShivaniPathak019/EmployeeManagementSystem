using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand(
    string EmployeeCode,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    DateTime DateOfBirth,
    DateTime DateOfJoining,
    decimal Salary,
    string? Address,
    int DepartmentId,
    int DesignationId) : IRequest<int>;