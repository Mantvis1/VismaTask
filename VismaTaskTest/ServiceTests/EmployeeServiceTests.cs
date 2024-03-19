using FluentValidation;
using Moq;
using VismaTask.Exceptions;
using VismaTask.Models;
using VismaTask.Models.DTOs;
using VismaTask.Repositories;
using VismaTask.Services;
using VismaTaskTests.DataGenerator;
using Xunit;

namespace VismaTaskTests.ServiceTests;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepository;
    private readonly Mock<IValidator<EmployeeDTO>> _employeeDTOValidator;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();
        _employeeDTOValidator = new Mock<IValidator<EmployeeDTO>>();

        _employeeService = new EmployeeService(
            _employeeRepository.Object,
            _employeeDTOValidator.Object);
    }

    [Theory]
    [InlineData(1, 3)]
    public void GetAll_EmployeeListHasMoreThanZeroEmployees_ReturnsOneEmployeeWithCorrectId(int firstEmployeeId, int employeeCount)
    {
        var testEmployees = EmployeeGenerator.Get(employeeCount);
        testEmployees[0].Id = firstEmployeeId;

        _employeeRepository.Setup(x => x.GetAll()).Returns(testEmployees);

        var employees = _employeeService.GetAll();

        Assert.Equal(employees.Count, employeeCount);
        Assert.Equal(firstEmployeeId, employees[0].Id);
    }


    [Fact]
    public void GetAll_EmployeeListHasZeroEmployees_ThrowsException()
    {
        _employeeRepository.Setup(x => x.GetAll()).Returns(EmployeeGenerator.Get(0));

        Assert.Throws<EmployeeNotFoundException>(_employeeService.GetAll);
    }


    [Theory]
    [InlineData(1)]
    public void GetById_ExistingEmployeeIdIsGiven_ReturnsCorrectEmployee(int id)
    {
        var testEmployee = EmployeeGenerator.Get(1)[0];
        testEmployee.Id = id;

        _employeeRepository.Setup(x => x.GetById(id)).Returns(testEmployee);

        var employee = _employeeService.GetById(id);

        Assert.NotNull(employee);
        Assert.Equal(employee.Id, id);
    }

    [Theory]
    [InlineData(10)]
    public void GetById_NonExistingEmployeeIdIsGiven_ThrowsException(int id)
    {
        _employeeRepository.Setup(x => x.GetById(id)).Returns((Employee)null);

        Assert.Throws<EmployeeNotFoundException>(() => _employeeService.GetById(id));
    }

    [Theory]
    [InlineData(1, -1000.50)]
    public void UpdateSalary_ThenEmployeeSalaryDtoIsInvalid_ThrowsArgumentException(int id, double expectedSalary)
    {
        Assert.Throws<ArgumentException>(() => _employeeService.UpdateSalary(id, expectedSalary));
    }

    [Theory]
    [InlineData(1, 1000.50)]
    public void UpdateSalary_ThenEmployeeSalaryDtoIsValid_SalaryUpdatedSuccessful(int id, double expectedSalary)
    {
        var testEmployee = EmployeeGenerator.Get(1)[0];
        _employeeRepository.Setup(x => x.GetById(id)).Returns(testEmployee);

        var employee = _employeeService.UpdateSalary(id, expectedSalary);

        Assert.Equal(employee.CurrentSalary, expectedSalary);
    }

    [Fact]
    public void GroupByRole_When2RolesWithSalaryExists_GrouppedCorrectly()
    {
        _employeeRepository.Setup(x => x.GetAll()).Returns(EmployeeDataSeeder.EmployeeDataForGroupByRoleTest());

        var groupedEmployees = _employeeService.GroupByRole();

        Assert.Equal("FirstRole", groupedEmployees[0].Role);
        Assert.Equal("SecondRole", groupedEmployees[1].Role);
        Assert.Equal(600, groupedEmployees[0].AverageSalary);
        Assert.Equal(500, groupedEmployees[1].AverageSalary);
        Assert.Equal(2, groupedEmployees[0].Count);
        Assert.Equal(1, groupedEmployees[1].Count);
    }

    [Fact]
    public void GroupByRole_When0EmployeesExists_ReturnsEmptyList()
    {
        var employees = EmployeeGenerator.Get(0);
        _employeeRepository.Setup(x => x.GetAll()).Returns(employees);

        var groupedEmployees = _employeeService.GroupByRole();

        Assert.Empty(groupedEmployees);
    }

    [Theory]
    [InlineData(null, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 0)]
    public void GetByBossId_WhenBossIdIsGiven_CountEmployeesWithGivenBossId(int? bossId, int employeesCount)
    {
        _employeeRepository.Setup(x => x.GetAll()).Returns(EmployeeDataSeeder.EmployeeDataForGetByBossIdTest());

        var employees = _employeeService.GetByBossId(bossId);

        Assert.Equal(employees.Count, employeesCount);
    }

    [Theory]
    [InlineData("Name", "1995-01-01", "2005-01-01")]
    public void GetByNameAndBirthday_When6EmployeesAreGiven_RetunsOnly1Employee(string name, string date1, string date2)
    {
        var dateTimes = new DateTime[2] 
        {
            DateTime.Parse(date1),
            DateTime.Parse(date2)
        };

        _employeeRepository.Setup(x => x.GetAll()).Returns(EmployeeDataSeeder.EmployeeDataForGetByNameAndBirthday());

        var employees = _employeeService.GetByNameAndBirthday(name, dateTimes);

        Assert.Single(employees);
    }
}