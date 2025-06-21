using EmployeeAdminPortal.Data.Models.Entities;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Data.Interfaces
{
    public interface IManagerRepository
    {
        List<Manager> GetAll();
        Manager Add(Manager manager);
        List<Employee> GetEmployeesByManagerId(Guid managerId);
    }
}
