namespace EmployeeManagement.Application.Employees.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfJoining { get; set; }
    public decimal Salary { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }

    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;

    public int DesignationId { get; set; }
    public string DesignationTitle { get; set; } = string.Empty;
}