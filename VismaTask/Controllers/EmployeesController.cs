using Microsoft.AspNetCore.Mvc;
using VismaTask.Exceptions;
using VismaTask.Models;
using VismaTask.Models.DTOs;
using VismaTask.Services;

namespace VismaTask.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet("{id}")]
    public ActionResult<Employee> Get(int id)
    {
        try
        {
            return Ok(_employeeService.GetById(id));
        }
        catch (EmployeeNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("FilterBy/Name&BirthDate")]
    public ActionResult<List<Employee>> GetByNameAndBirthDate(EmployeeNameAndDateDto employeeNameAndDateDto)
    {
        return Ok(_employeeService.GetByNameAndBirthday(employeeNameAndDateDto.Name, employeeNameAndDateDto.DateTimes));
    }

    [HttpGet("")]
    public ActionResult<List<Employee>> GetAll()
    {
        try
        {
            return Ok(_employeeService.GetAll());
        }
        catch (EmployeeNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("FilterBy/{bossId?}")]
    public ActionResult<List<Employee>> GetByBossId(int? bossId = null)
    {
        return Ok(_employeeService.GetByBossId(bossId));

    }

    [HttpGet("GroupBy/Role")]
    public ActionResult<List<EmployeeRoleDto>> GetByRole()
    {
        return Ok(_employeeService.GroupByRole());
    }

    [HttpPut("New")]
    public ActionResult<Employee> Put(EmployeeDTO employeeDTO)
    {
        try
        {
            return Ok(_employeeService.Add(employeeDTO));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Update/{id}")]
    public ActionResult<Employee> Update(int id, EmployeeDTO employeeDTO)
    {
        return Ok(_employeeService.Update(id, employeeDTO));
    }

    [HttpPost("Update/{id}/Salary")]
    public ActionResult<Employee> UpdateSalary(int id, double salary)
    {
        try
        {
            return Ok(_employeeService.UpdateSalary(id, salary));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (EmployeeNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            _employeeService.Delete(id);
            return NoContent();
        }
        catch (EmployeeNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}

