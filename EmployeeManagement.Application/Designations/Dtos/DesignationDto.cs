namespace EmployeeManagement.Application.Designations.Dtos;

public class DesignationDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}