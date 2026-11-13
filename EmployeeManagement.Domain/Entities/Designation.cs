namespace EmployeeManagement.Domain.Entities
{
    public class Designation
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}