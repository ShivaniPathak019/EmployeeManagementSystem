namespace EmployeeManagement.Domain.Entities
{
    public class DepartmentDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<EmployeeDetails> Employees { get; set; } = new List<EmployeeDetails>();
    }
}