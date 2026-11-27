using MediatR;

using EmployeeManagement.Application.Employees.Dtos;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployeePatch;

public class UpdateEmployeePatchCommand : IRequest<EmployeeDto>
{
    public int Id { get; set; }
    public string? EmployeeCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsActive { get; set; }
    public decimal? Salary { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfJoining { get; set; }
    public string? Address { get; set; }
    public int? DepartmentId { get; set; }
    public int? DesignationId { get; set; }
}