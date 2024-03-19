using FluentValidation;
using VismaTask.Exceptions;
using VismaTask.Models;
using VismaTask.Models.DTOs;
using VismaTask.Repositories;

namespace VismaTask.Services;

public class EmployeeService(
    IEmployeeRepository employeeRepository,
    IValidator<EmployeeDTO> employeeDtoValidator) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IValidator<EmployeeDTO> _employeeDtoValidator = employeeDtoValidator;

    public List<Employee> GetAll()
    {
        var employees = _employeeRepository.GetAll().ToList();

        if (employees.Count is 0)
        {
            throw new EmployeeNotFoundException($"There is currently no employees");
        }

        return employees;
    }

    public Employee GetById(int id)
    {
        var employee = _employeeRepository.GetById(id);

        if (employee is null)
        {
            throw new EmployeeNotFoundException($"Employee with id={id} is not found");
        }

        return employee;
    }

    public Employee UpdateSalary(int id, double salary)
    {
        if (salary < 0)
        {
            throw new ArgumentException($"Property {salary} failed validation. Error was: Salary can not be negative");
        }

        var employee = GetById(id);

        employee.CurrentSalary = salary;
        _employeeRepository.Update(employee);

        return employee;
    }  

    public void Delete(int id)
    {
        _employeeRepository.Delete(GetById(id));
    }

    public List<Employee> GetByNameAndBirthday(string name, DateTime[] dateTimes)
    {
        return GetAll()
            .Where(x => x.FirstName == name &&
            x.Birthdate >= dateTimes[0] &&
            x.Birthdate <= dateTimes[1])
            .ToList();
    }

    public List<Employee> GetByBossId(int? bossId)
    {
        return GetAll()
            .Where(x => x.Boss == bossId)
            .ToList();
    }

    public List<EmployeeRoleDto> GroupByRole()
    {
        var employeeGroupDto = new List<EmployeeRoleDto>();
        var groupedEmployees = _employeeRepository.GetAll().GroupBy(x => x.Role).ToList();

        foreach (var groupedEmployee in groupedEmployees)
        {
            employeeGroupDto.Add(new EmployeeRoleDto
            {
                Role = groupedEmployee.FirstOrDefault().Role,
                Count = groupedEmployee.Count(),
                AverageSalary = groupedEmployee.Average(x => x.CurrentSalary)
            });

        }

        return employeeGroupDto;
    }

    public Employee Add(EmployeeDTO employeeDTO)
    {
    /*    var validationResults = _employeeDtoValidator.Validate(employeeDTO);

        if (!validationResults.IsValid)
        {
            throw new ArgumentException($"Property {validationResults.Errors[0].PropertyName} failed validation. Error was: {validationResults.Errors[0].ErrorMessage}");
        }*/

        var employee = new Employee()
        {
            FirstName = employeeDTO.FirstName,
            LastName = employeeDTO.LastName,
            EmploymentDate = employeeDTO.EmploymentDate,
            Birthdate = employeeDTO.Birthdate,
            Boss = employeeDTO.Boss,
            CurrentSalary = employeeDTO.CurrentSalary,
            HomeAddress = employeeDTO.HomeAddress,
            Role = employeeDTO.Role
        };

        employee.FirstName = employeeDTO.FirstName;

        _employeeRepository.Add(employee);

        return employee;
    }

    public Employee Update(int id, EmployeeDTO employeeDTO)
    {
        var employee = GetById(id);

        if (employeeDTO.FirstName == employeeDTO.LastName)
        {
            throw new Exception(); // Names cant not be equal exception
        }

        // if()

        employee.FirstName = employeeDTO.FirstName;
        employee.LastName = employeeDTO.LastName;
        employee.Birthdate = employeeDTO.Birthdate;
        employee.CurrentSalary = employeeDTO.CurrentSalary;
        employee.Role = employeeDTO.Role;
        employee.EmploymentDate = employeeDTO.EmploymentDate;
        employee.Boss = employeeDTO.Boss;

        _employeeRepository.Update(employee);

        return employee;
    }
}
