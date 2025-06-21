using AutoMapper;
using EmployeeAdminPortal.Entity.Dto;
using EmployeeAdminPortal.Services.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    private readonly IMapper _mapper;
    //private readonly IEmployeeService _service;

    public EmployeesController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult GetAllEmployees() 
    {
        var employees = _employeeService.GetAllEmployees();
        var response = employees.Select(e => new {
            e.ID,
            e.Name,
            e.Email,
            e.Phone,
            e.Salary,
            ManagerName = e.Manager?.Name ?? "No Manager"
        });
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult Create(AddEmployeeDto dto)
    {
        var employee = new EmployeeAdminPortal.Models.Entities.Employee
        {   
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Salary = dto.Salary,
            ManagerId = dto.ManagerId
        };
        string temp=_employeeService.CreateEmployee(employee);
        if (temp == "Employee created successfully.")
        {
            return Ok(employee);
        }
        else {
            return BadRequest(temp); }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateEmployeeDto dto)
    {
        var employee = _employeeService.GetEmployeeById(id);
        if (employee == null) return NotFound();

        employee.Name = dto.Name;
        employee.Email = dto.Email;
        employee.Phone = dto.Phone;
        employee.Salary = dto.Salary;
        employee.ManagerId = dto.ManagerId;

        _employeeService.UpdateEmployee(employee);
        return Ok(employee);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        if (employee == null) return NotFound();

        _employeeService.DeleteEmployee(employee);
        return Ok();
    }
}
