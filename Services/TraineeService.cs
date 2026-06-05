namespace TraineeManagementApi.Services;

using TraineeManagementApi.Interfaces;

using TraineeManagementApi.DTO;

using TraineeManagementApi.Models;


public class TraineeService : ITraineeService
{
    private static List<Trainee> trainees = [];
    private static long id = 0;

    public Result<List<Trainee>> GetAll()
    {
        return Result<List<Trainee>>.Success(trainees);
    }

    public Result<TraineeResponse> GetById(long id)
    {
        var trainee = trainees.FirstOrDefault(t => t.Id == id);
        if (trainee == null)
        {
            return Result<TraineeResponse>.Failure("Trainee not found.");
        }
        TraineeResponse response = new TraineeResponse()
        {
            Id = trainee.Id,
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            Email = trainee.Email,
            TechStack = trainee.TechStack,
            Status = trainee.Status,
            CreatedDate = trainee.CreatedDate,
            UpdatedDate = trainee.UpdatedDate,
        };
        return Result<TraineeResponse>.Success(response);
    }

    public Result<long> PostById( CreateTraineeRequest trainee)
    {
        id += 1;
        if (trainee.Id == 0)
        {
            trainee.Id = id;
        } 
        Trainee trainee1 = new Trainee()
        {
            Id = trainee.Id,
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            Email = trainee.Email,
            TechStack = trainee.TechStack,
            Status = trainee.Status,
        };
        trainees.Add(trainee1);
        return Result<long>.Success(id);
    }

    public Result<long> PutById(UpdateTraineeRequest trainee)
    {
        var old_trainee = trainees.FirstOrDefault(t => t.Id == id);
        if (trainee == null)
        {
            return Result<long>.Failure("Trainee not found.");
        }
        try
        {
            old_trainee.Id = trainee.Id;
            old_trainee.FirstName = trainee.FirstName;
            old_trainee.LastName = trainee.LastName;
            old_trainee.Email = trainee.Email;
            old_trainee.TechStack = trainee.TechStack;
            old_trainee.Status = trainee.Status;
            old_trainee.UpdatedDate = trainee.UpdatedDate;
            return Result<long>.Success(200);
            
        }
        catch
        {
            return Result<long>.Failure("Invalid data");
        }
    }


    public Result<long> DeleteById(long id)
    {
        int index = trainees.FindIndex(t => t.Id == id);
        if (index == -1)
        {
            return Result<long>.Failure("Id not found");
        }
        trainees.RemoveAt(index);
        return Result<long>.Success(200);
    }

}