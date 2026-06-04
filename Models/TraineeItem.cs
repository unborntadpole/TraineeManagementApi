namespace TraineeManagementApi.Models;

public class Trainee
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? TechStack { get; set; }
    public string? Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

}