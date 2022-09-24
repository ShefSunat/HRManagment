namespace Domain.Models;
public class Employees
{
    public DateTime Birthdate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateTime HireDate { get; set; }
    public Int64 Id { get; set; }
}