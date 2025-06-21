namespace EmployeeAdminPortal.Entity.Dto
{
    public class UpdateEmployeeDto
    {
        public required string Name { get; set; }
        //required is used for the nonnullable column

        public string Email { get; set; }
        public string? Phone { get; set; }
        // ? refers the non nullable one 
        public decimal Salary { get; set; }

        public Guid? ManagerId { get; set; }
    }
}
