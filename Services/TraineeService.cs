namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

using TraineeManagementApi.Models;

using TraineeManagementApi.db;


public class TraineeService : ITraineeService
{
    private readonly ITraineeRepository _repository;

    public TraineeService(ITraineeRepository repository)
    {
        _repository = repository;
    }


    public async Task<Result<List<TraineeResponse>>> GetAll(string? search)
    {
        List<TraineeResponse> trainees2 = [];

        var trainees = await _repository.GetAllAsync(search);
        foreach (var trainee in trainees)
        {
            TraineeResponse trainee2 = new TraineeResponse(trainee);
            trainees2.Add(trainee2);
        }
        return Result<List<TraineeResponse>>.Success(trainees2);
    }

    public async Task<Result<TraineeResponse>> GetById(long id)
    {
        var trainee = await _repository.GetByIdAsync(id);
        if (trainee == null)
        {
            return Result<TraineeResponse>.Failure("Trainee not found.");
        }
        // TraineeResponse response = new TraineeResponse()
        // {
        //     Id = trainee.Id,
        //     FirstName = trainee.FirstName,
        //     LastName = trainee.LastName,
        //     Email = trainee.Email,
        //     TechStack = trainee.TechStack,
        //     Status = trainee.Status,
        //     CreatedDate = trainee.CreatedDate,
        //     UpdatedDate = trainee.UpdatedDate,
        // };
        TraineeResponse response = new TraineeResponse(trainee);
        return Result<TraineeResponse>.Success(response);
    }

    public async Task<Result<long>> PostById( CreateTraineeRequest trainee)
    {
        // Trainee trainee1 = new Trainee()
        // {
        //     Id = trainee.Id,
        //     FirstName = trainee.FirstName,
        //     LastName = trainee.LastName,
        //     Email = trainee.Email,
        //     TechStack = trainee.TechStack,
        //     Status = trainee.Status,
        // };
        Trainee trainee1 = new Trainee(trainee);
        await _repository.AddAsync(trainee1);
        await _repository.SaveChangesAsync();
        return Result<long>.Success(200);
    }

    public async Task<Result<long>> PutById(UpdateTraineeRequest trainee)
    {
        var old_trainee = await _repository.GetByIdAsync(trainee.Id);
        if (old_trainee == null)
        {
            return Result<long>.Failure("Trainee not found.");
        }
        try
        {
            old_trainee.FirstName = trainee.FirstName;
            old_trainee.LastName = trainee.LastName;
            old_trainee.Email = trainee.Email;
            old_trainee.TechStack = trainee.TechStack;
            old_trainee.Status = trainee.Status;
            old_trainee.UpdatedDate = DateTime.UtcNow;
            _repository.Update(old_trainee);
            await _repository.SaveChangesAsync();
            return Result<long>.Success(200);
            
        }
        catch
        {
            return Result<long>.Failure("Invalid data");
        }
    }


    public async Task<Result<long>> DeleteById(long id)
    {
        var trainee = await _repository.GetByIdAsync(id);
        if (trainee == null)
        {
            return Result<long>.Failure("Id not found");
        }
        _repository.Delete(trainee);
        await _repository.SaveChangesAsync();
        return Result<long>.Success(200);
    }

}


// {
//     private static List<Trainee> trainees = [];
//     private static long id = 0;

//     public Result<List<TraineeResponse>> GetAll()
//     {
//         List<TraineeResponse> trainees2 = [];
//         foreach (var trainee in trainees)
//         {
//             TraineeResponse trainee2 = new TraineeResponse()
//             {
//                 Id = trainee.Id,
//                 FirstName = trainee.FirstName,
//                 LastName = trainee.LastName,
//                 Email = trainee.Email,
//                 TechStack = trainee.TechStack,
//                 Status = trainee.Status,
//                 CreatedDate = trainee.CreatedDate,
//                 UpdatedDate = trainee.UpdatedDate,
//             };
//             trainees2.Add(trainee2);
//         }
//         return Result<List<TraineeResponse>>.Success(trainees2);
//     }

//     public Result<TraineeResponse> GetById(long id)
//     {
//         var trainee = trainees.FirstOrDefault(t => t.Id == id);
//         if (trainee == null)
//         {
//             return Result<TraineeResponse>.Failure("Trainee not found.");
//         }
//         TraineeResponse response = new TraineeResponse()
//         {
//             Id = trainee.Id,
//             FirstName = trainee.FirstName,
//             LastName = trainee.LastName,
//             Email = trainee.Email,
//             TechStack = trainee.TechStack,
//             Status = trainee.Status,
//             CreatedDate = trainee.CreatedDate,
//             UpdatedDate = trainee.UpdatedDate,
//         };
//         return Result<TraineeResponse>.Success(response);
//     }

//     public Result<long> PostById( CreateTraineeRequest trainee)
//     {
//         id += 1;
//         if (trainee.Id == 0)
//         {
//             trainee.Id = id;
//         } 
//         Trainee trainee1 = new Trainee()
//         {
//             Id = trainee.Id,
//             FirstName = trainee.FirstName,
//             LastName = trainee.LastName,
//             Email = trainee.Email,
//             TechStack = trainee.TechStack,
//             Status = trainee.Status,
//         };
//         trainees.Add(trainee1);
//         return Result<long>.Success(id);
//     }

//     public Result<long> PutById(UpdateTraineeRequest trainee)
//     {
//         var old_trainee = trainees.FirstOrDefault(t => t.Id == id);
//         if (trainee == null)
//         {
//             return Result<long>.Failure("Trainee not found.");
//         }
        // try
        // {
        //     old_trainee.Id = trainee.Id;
        //     old_trainee.FirstName = trainee.FirstName;
        //     old_trainee.LastName = trainee.LastName;
        //     old_trainee.Email = trainee.Email;
        //     old_trainee.TechStack = trainee.TechStack;
        //     old_trainee.Status = trainee.Status;
        //     old_trainee.UpdatedDate = trainee.UpdatedDate;
        //     return Result<long>.Success(200);
            
        // }
//         catch
//         {
//             return Result<long>.Failure("Invalid data");
//         }
//     }


//     public Result<long> DeleteById(long id)
//     {
//         int index = trainees.FindIndex(t => t.Id == id);
//         if (index == -1)
//         {
//             return Result<long>.Failure("Id not found");
//         }
//         trainees.RemoveAt(index);
//         return Result<long>.Success(200);
//     }

// }