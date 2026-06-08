using TraineeManagementApi.Models;

using Microsoft.EntityFrameworkCore;

namespace TraineeManagementApi.db;

public interface ITraineeRepository
{
    Task<IEnumerable<Trainee>> GetAllAsync();
    Task<Trainee?> GetByIdAsync(long id);
    Task AddAsync(Trainee trainee);
    void Update(Trainee trainee);
    void Delete(Trainee trainee);
    Task<bool> SaveChangesAsync();
}
