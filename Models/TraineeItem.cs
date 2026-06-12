using TraineeManagementApi.DTO;
namespace TraineeManagementApi.Models;

public class Trainee
{
    private static long uuid = 1;
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string TechStack { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public Trainee(CreateTraineeRequest trainee)
    {
        // Id = uuid;
        FirstName = trainee.FirstName;
        LastName = trainee.LastName;
        Email = trainee.Email;
        TechStack = trainee.TechStack;
        Status = trainee.Status;
        uuid++;
    }
    public Trainee() {}

}