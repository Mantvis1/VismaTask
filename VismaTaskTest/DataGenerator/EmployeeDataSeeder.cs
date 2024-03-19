using VismaTask.Models;

namespace VismaTaskTests.DataGenerator;

public static class EmployeeDataSeeder
{
    public static List<Employee> EmployeeDataForGroupByRoleTest()
    {
        var employees = EmployeeGenerator.Get(3);
        employees[0].Role = "FirstRole";
        employees[1].Role = "FirstRole";
        employees[2].Role = "SecondRole";

        employees[0].CurrentSalary = 1000;
        employees[1].CurrentSalary = 200;
        employees[2].CurrentSalary = 500;

        return employees;
    }

    public static List<Employee> EmployeeDataForGetByBossIdTest()
    {
        var employees = EmployeeGenerator.Get(3);

        employees[0].Boss = null;
        employees[1].Boss = 1;
        employees[2].Boss = 1;

        return employees;
    }

    public static List<Employee> EmployeeDataForGetByNameAndBirthday()
    {
        var employees = EmployeeGenerator.Get(6);

        employees[0].FirstName = "Name";
        employees[0].Birthdate = DateTime.Parse("2000-01-01");

        employees[1].FirstName = "Name";
        employees[1].Birthdate = DateTime.Parse("2010-01-01");

        employees[2].FirstName = "Name";
        employees[2].Birthdate = DateTime.Parse("1900-01-01");

        employees[3].FirstName = "WrongName";
        employees[3].Birthdate = DateTime.Parse("2000-01-01");

        employees[4].FirstName = "WrongName";
        employees[4].Birthdate = DateTime.Parse("1900-01-01");

        employees[5].FirstName = "WrongName";
        employees[5].Birthdate = DateTime.Parse("2010-01-01");

        return employees;
    }

    
}
