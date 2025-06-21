using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Data.Data;
using EmployeeAdminPortal.Data.Interfaces;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ManagerRepository(ApplicationDbContext DbContext)
        {
            dbContext = DbContext;
        }

        public List<Manager> GetAll()
        {
            return dbContext.Managers.ToList();
        }

        public Manager? Add(Manager manager)
        {
            dbContext.Managers.Add(manager);
            dbContext.SaveChanges();
            return manager;
        }

        public List<Employee> GetEmployeesByManagerId(Guid managerId)
        {
            return dbContext.Employees
                .Include(e => e.Manager)
                .Where(e => e.ManagerId == managerId)
                .ToList();
        }

    }
}
