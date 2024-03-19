using VismaTask.Models;

namespace VismaTask.Repositories;

public interface IEmployeeRepository
{
    List<Employee> GetAll();
    void Delete(Employee employee);
    void Update(Employee employee);
    void Add(Employee employee);
    Employee? GetById(int id);
}