using EmployeeAdminPortal.Data.Data;
using EmployeeAdminPortal.Data.Interfaces;
using EmployeeAdminPortal.Data.Models.Entities;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        //contructoor for get the data of the database
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Employee> GetAll()
        {

            try
            {
                return dbContext.Employees.Include(e => e.Manager).ToList();
            }
            catch (Exception ex) { 
                throw new Exception("problem in GetALL() orgin",ex); 
            }
           
        }
        public Employee? GetById(Guid id) =>
        dbContext.Employees.FirstOrDefault(e => e.ID == id);

        public bool Add(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges(); 
             return true;
        }

        public void Update(Employee employee) => dbContext.Employees.Update(employee);

        public void Delete(Employee employee) { dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
        }

        public List<Employee> GetByManagerId(Guid managerId) =>
      dbContext.Employees.Where(e => e.ManagerId == managerId).ToList();

    }
}
