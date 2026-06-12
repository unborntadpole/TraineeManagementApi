namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

public interface ITraineeService
{

    Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery);
    Task<Result<TraineeResponse>> GetById(long id);
    Task<Result<long>> PostById( CreateTraineeRequest trainee);
    Task<Result<long>> PutById(UpdateTraineeRequest trainee);

    Task<Result<long>> DeleteById(long id);
}