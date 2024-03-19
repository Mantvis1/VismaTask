using System.ComponentModel.DataAnnotations;

namespace VismaTask.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(50, ErrorMessage = "FirstName cannot be longer than 50 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    [StringLength(50, ErrorMessage = "LastName cannot be longer than 50 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Birthdate is required")]
    public DateTime Birthdate { get; set; }

    [Required(ErrorMessage = "EmploymentDate is required")]
    public DateTime EmploymentDate { get; set; }

    [Required(ErrorMessage = "HomeAddress is required")]
    public string HomeAddress { get; set; }

    [Required(ErrorMessage = "CurrentSalary is required")]
    public double CurrentSalary { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }

    public int? Boss {get; set; }
}
