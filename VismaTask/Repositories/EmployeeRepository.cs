using EFCoreInMemoryDbDemo;
using VismaTask.Models;

namespace VismaTask.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApiContext _context;

    public EmployeeRepository(ApiContext context)
    {
        _context = context;
        Seed();
    }

    public List<Employee> GetAll()
    {
        return 
            _context
            .Employees
            .ToList();
    }

    public Employee? GetById(int id)
    {
        return 
            _context
            .Employees
            .FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Employee employee)
    {
        _context
            .Employees
            .Remove(employee);

        _context.SaveChanges();
    }

    private void Seed()
    {
        if (!_context.Employees.Any())
        {
            _context.Add(new Employee { 
                Id = 1,
                FirstName="aaaaaaaaa",
                LastName="cxxx",
                CurrentSalary = 1000,
                Role = "CEO", 
                HomeAddress = "SASASSAASASSA",
                Birthdate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Boss = null,
            });

            _context.Add(new Employee
            {
                Id = 2,
                FirstName = "aaaaaaaaa",
                LastName = "khjkhjk",
                CurrentSalary = 500,
                Role = "gfdsgdfgdfg",
                HomeAddress = "hfghfghfg",
                Birthdate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Boss = 1,
            });

            _context.Add(new Employee
            {
                Id = 3,
                FirstName = "aaaaaaaaa",
                LastName = "khjkhjk",
                CurrentSalary = 10000000,
                Role = "gfdsgdfgdfg",
                HomeAddress = "hfghfghfg",
                Birthdate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Boss = 1,
            });

            _context.SaveChanges();
        }
    }

    public void Update(Employee employee)
    {
        _context.Update(employee);
        _context.SaveChanges();
    }

    public void Add(Employee employee)
    {
        _context.Add(employee);
        _context.SaveChanges();
    }
}
