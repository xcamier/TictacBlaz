using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.Interfaces;

namespace tictacApp.Helpers;

public class ActorsServiceHelper
{
    public static async Task<Actor?> GetActorByDefaultForTimelogs(IGenericCRUDService crud)
    {
        using var context = crud.GetNewDBContext();

        Actor? actor = await context.Actors.FirstOrDefaultAsync(a => a.UseAsDefault == true);

        return actor;
    }
}