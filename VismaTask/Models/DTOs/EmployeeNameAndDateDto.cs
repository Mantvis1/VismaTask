namespace VismaTask.Models.DTOs;

public class EmployeeNameAndDateDto
{
    public required string Name { get; set; }
    public required DateTime[] DateTimes { get; set; }   
}
