namespace TraineeManagementApi.Interfaces;

using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

public interface ITraineeService
{

    Result<List<Trainee>> GetAll();
    Result<TraineeResponse> GetById(long id);
    Result<long> PostById( CreateTraineeRequest trainee);
    Result<long> PutById(UpdateTraineeRequest trainee);

    Result<long> DeleteById(long id);
}