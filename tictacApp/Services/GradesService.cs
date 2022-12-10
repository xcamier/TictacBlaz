using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class GradesService
{
    IDbContextFactory<TictacDBContext> _dbFactory;

    public GradesService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<Grade[]> GetGradesAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Grades.OrderBy(t => t.Label).ToArrayAsync();
    }

    public async Task<Grade?> FindGradeFromIdAsync(TictacDBContext? dbContext, int gradeId)
    {    
        if (dbContext is not null && dbContext.Grades is not null)
        {
            return await dbContext.Grades.SingleOrDefaultAsync(c => c.Id == gradeId); 
        }

        return null;
    }

    public async Task<bool> AddGradeAsync(Grade? gradeToAdd)
    {
        if (gradeToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Grades?.Add(gradeToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteGradeAsync(Grade gradeToDelete)
    {
        if (gradeToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(gradeToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }
}