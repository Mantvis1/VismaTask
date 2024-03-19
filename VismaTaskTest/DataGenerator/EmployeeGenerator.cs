using Bogus;
using VismaTask.Models;

namespace VismaTaskTests.DataGenerator;

public static class EmployeeGenerator
{
    public static List<Employee> Get(int count)
    {
        return new Faker<Employee>()
             .RuleFor(x => x.Id, x => x.Random.Number(0, 100))
             .RuleFor(x => x.FirstName, x => x.Name.FirstName())
             .RuleFor(x => x.LastName, x => x.Name.LastName())
             .Generate(count);
    }
}
