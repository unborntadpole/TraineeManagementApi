namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

using TraineeManagementApi.Models;

using TraineeManagementApi.db;


public class TraineeService : ITraineeService
{
    private readonly ITraineeRepository _repository;
    private readonly ILogger<TraineeService> _logger;

    public TraineeService(ITraineeRepository repository, ILogger<TraineeService> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery)
    {
        var trainees = await _repository.GetAllAsync(searchQuery.search, searchQuery.status, searchQuery.pageNumber, searchQuery.pageSize);
        if (trainees == null)
        {
            _logger.LogWarning("Trainee: Get All failed: No trainees found with given keywords");
            return Result<SearchResponse>.Failure("");
        }
        SearchResponse response = new SearchResponse(trainees, searchQuery.pageNumber, searchQuery.pageSize);
        _logger.LogInformation("Trainee: Get All LogInformation");
        return Result<SearchResponse>.Success(response);
    }

    public async Task<Result<TraineeResponse>> GetById(long id)
    {
        var trainee = await _repository.GetByIdAsync(id);
        if (trainee == null)
        {
            _logger.LogWarning("Trainee: Get with Id failed: No trainee found with given Id");
            return Result<TraineeResponse>.Failure("Trainee not found.");
        }
        TraineeResponse response = new TraineeResponse(trainee);
        _logger.LogInformation("Trainee: Get with Id successful");
        return Result<TraineeResponse>.Success(response);
    }

    public async Task<Result<long>> PostById( CreateTraineeRequest trainee)
    {
        Trainee trainee1 = new Trainee(trainee);
        await _repository.AddAsync(trainee1);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Trainee: Post successful");
        return Result<long>.Success(200);
    }

    public async Task<Result<long>> PutById(UpdateTraineeRequest trainee)
    {
        var old_trainee = await _repository.GetByIdAsync(trainee.Id);
        if (old_trainee == null)
        {
            _logger.LogWarning("Trainee: Put by Id failed - Id not found");
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
            _logger.LogInformation("Trainee: Put by Id successful");
            return Result<long>.Success(200);
            
        }
        catch
        {
            _logger.LogWarning("Trainee: Put by Id failed - Invalid Data");
            return Result<long>.Failure("Invalid data");
        }
    }


    public async Task<Result<long>> DeleteById(long id)
    {
        var trainee = await _repository.GetByIdAsync(id);
        if (trainee == null)
        {
            _logger.LogWarning("Trainee: Delete by Id failed - Id not found");
            return Result<long>.Failure("Id not found");
        }
        _repository.Delete(trainee);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Trainee: Delete by Id successful");
        return Result<long>.Success(200);
    }

}
