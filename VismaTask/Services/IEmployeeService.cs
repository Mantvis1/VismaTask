using VismaTask.Models;
using VismaTask.Models.DTOs;

namespace VismaTask.Services;

public interface IEmployeeService
{
    Employee GetById(int id);
    List<Employee> GetAll();
    Employee UpdateSalary(int id, double salary);
    Employee Add(EmployeeDTO employeeDTO);
    void Delete(int id);
    List<Employee> GetByNameAndBirthday(string name, DateTime[] dateTimes);
    List<Employee> GetByBossId(int? bossId);
    List<EmployeeRoleDto> GroupByRole();
    Employee Update(int id, EmployeeDTO employeeDTO);
}