using EmployeeAdminPortal.Data.Interfaces;
using EmployeeAdminPortal.Data.Models.Entities;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Services.Services
{
    public class ManagerService
    {
        private readonly IManagerRepository _repository;

        public ManagerService(IManagerRepository repository)
        {
            _repository = repository;
        }

        public List<Manager> GetAllManagers()
        {
            return _repository.GetAll();
        }

        public Manager? AddManager(string name)
        {
            var manager = new Manager { Name = name };
            return _repository.Add(manager);
        }

        public List<Employee> GetEmployeesByManagerId(Guid id)
        {
            return _repository.GetEmployeesByManagerId(id);
        }
    }
}
