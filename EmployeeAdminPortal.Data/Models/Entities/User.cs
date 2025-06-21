using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Data.Models.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
    }
}
