namespace VismaTask.Models.DTOs;

public class EmployeeDTO
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public DateTime EmploymentDate { get; set; }

    public string HomeAddress { get; set; }

    public double CurrentSalary { get; set; }

    public string Role { get; set; }

    public int? Boss { get; set; }
}