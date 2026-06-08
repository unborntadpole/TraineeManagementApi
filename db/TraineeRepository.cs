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

    public async Task<IEnumerable<Trainee>> GetAllAsync(string? search)
    {
        var query = await _context.Trainees.ToListAsync();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = 
            (from trainee in query 
            where trainee.FirstName.ToLower().Contains(search) 
            || trainee.LastName.ToLower().Contains(search)
            || trainee.Email.ToLower().Contains(search)
            || trainee.TechStack.ToLower().Contains(search)
            select trainee) 
            .ToList();
        }
        
        return query;
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
