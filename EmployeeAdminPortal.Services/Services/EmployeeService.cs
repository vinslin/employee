using EmployeeAdminPortal.Data.Interfaces;
using EmployeeAdminPortal.Data.Models.Entities;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Services.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Service Error: Failed to fetch all employees.", ex);
            }
        }

        public Employee? GetEmployeeById(Guid id)
        {
            try
            {
                return _repo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Service Error: Failed to fetch employee with ID {id}.", ex);
            }
        }

        public string CreateEmployee(Employee employee)
        {
            try
            {
                if (!IsValidEmail(employee.Email))
                {
                    return "Invalid email format.";
                }

                if (!IsValidPhoneNumber(employee.Phone))
                {
                    return "Invalid phone number. It must be exactly 10 digits.";
                }

                var existingEmployees = _repo.GetAll();

                if (existingEmployees.Any(e => e.Email == employee.Email))
                {
                    return "Email is already in use.";
                }

                if (existingEmployees.Any(e => e.Phone == employee.Phone))
                {
                    return "Phone number is already in use.";
                }

                _repo.Add(employee);
                return "Employee created successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception("Service Error: Failed to create employee.", ex);
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                _repo.Update(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("Service Error: Failed to update employee.", ex);
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            try
            {
                _repo.Delete(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("Service Error: Failed to delete employee.", ex);
            }
        }

        public List<Employee> GetEmployeesByManager(Guid managerId)
        {
            try
            {
                return _repo.GetByManagerId(managerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Service Error: Failed to get employees for manager with ID {managerId}.", ex);
            }
        }

        // 📌 Helper Methods
        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{10}$");
        }
    }
}
