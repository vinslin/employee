using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models.Entities
{
    public class Manager
    {
        // [Key]
        public Guid ManagerId { get; set; }

        public required string Name { get; set; } = string.Empty;

        // Optional: Navigation - list of employees under this manager
        public Employee? Employee { get; set; }
    }
}
