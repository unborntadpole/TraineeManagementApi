using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.db;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.db;

public class TraineeRepository : ITraineeRepository
{
    private readonly ApplicationDbContext _context;

    public TraineeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trainee>> GetAllAsync()
    {
        return await _context.Trainees.ToListAsync();
    }

    public async Task<Trainee?> GetByIdAsync(long id)
    {
        return await _context.Trainees.FindAsync(id);
    }

    public async Task AddAsync(Trainee trainee)
    {
        await _context.Trainees.AddAsync(trainee);
    }

    public void Update(Trainee trainee)
    {
        _context.Trainees.Update(trainee);
    }

    public void Delete(Trainee trainee)
    {
        _context.Trainees.Remove(trainee);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
