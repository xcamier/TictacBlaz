using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class CharacteristicsGroupsService
{
    IDbContextFactory<TictacDBContext> _dbFactory;

    public CharacteristicsGroupsService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<CharacteristicsGroup[]> GetCharacteristicsGroupsAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.CharacteristicsGroups.OrderBy(t => t.Label).ToArrayAsync();
    }

    public async Task<CharacteristicsGroup?> FindCharacteristicsGroupFromIdAsync(TictacDBContext? dbContext, int characteristicsGroup)
    {    
        if (dbContext is not null && dbContext.CharacteristicsGroups is not null)
        {
            return await dbContext.CharacteristicsGroups.SingleOrDefaultAsync(c => c.Id == characteristicsGroup); 
        }

        return null;
    }

    public async Task<bool> AddCharacteristicsGroupAsync(CharacteristicsGroup? characteristicsGroupToAdd)
    {
        if (characteristicsGroupToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.CharacteristicsGroups?.Add(characteristicsGroupToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteCharacteristicsGroupAsync(CharacteristicsGroup characteristicsGroupToDelete)
    {
        if (characteristicsGroupToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(characteristicsGroupToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }
}