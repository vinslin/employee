using EmployeeAdminPortal.Entity.Dto;



//using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ManagersController : ControllerBase
    {
        private readonly ManagerService _service;

        public ManagersController(ManagerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllManagers()
        {
            var managers = _service.GetAllManagers();
            return Ok(managers);
        }

        [HttpPost]
        public IActionResult AddManager(AddManagerDto dto)
        {
            var manager = _service.AddManager(dto.Name);
            return Ok(manager);
        }

        [HttpGet("{id}/employees")]
        public IActionResult GetEmployeesByManager(Guid id)
        {
            var employees = _service.GetEmployeesByManagerId(id);
            if (employees == null || employees.Count == 0)
                return NotFound("No employees found for this manager");

            return Ok(employees);
        }
    }
}
