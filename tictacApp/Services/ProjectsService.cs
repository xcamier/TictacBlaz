using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class ProjectsService
{
    IDbContextFactory<TictacDBContext> _dbFactory;

    public ProjectsService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<Project[]> GetProjectsAsync(int? parentId)
    {
        using var context = _dbFactory.CreateDbContext();
        
        if (parentId.HasValue)
        {
            return await context.Projects.
                                    Where(p => p.ParentProject != null && p.ParentProjectId == parentId).
                                    OrderBy(t => t.Label).ToArrayAsync();
        }
        else
        {
            return await context.Projects.
                                    Where(p => p.ParentProject == null).
                                    OrderBy(t => t.Label).ToArrayAsync();
        }
    }

    public async Task<Project?> FindProjectFromIdAsync(TictacDBContext? dbContext, int projectId)
    {    
        if (dbContext is not null && dbContext.Projects is not null)
        {
            return await dbContext.Projects.SingleOrDefaultAsync(c => c.Id == projectId); 
        }

        return null;
    }

    public async Task<KeyValuePair<int, string?>[]> GetParentProjects(int projectId)
    {
        List<KeyValuePair<int, string?>> parentProjects = new List<KeyValuePair<int, string?>>();

        using var context = _dbFactory.CreateDbContext();

        int? projectIdToSearch = projectId;
        while (projectIdToSearch.HasValue)
        {
            Project? foundProject = await context.Projects.SingleOrDefaultAsync(p => p.Id == projectIdToSearch.Value);
            if (foundProject is not null)
            {
                KeyValuePair<int, string?> item = new KeyValuePair<int, string?>(foundProject.Id, foundProject.Label);
                parentProjects.Insert(0, item);

                projectIdToSearch = foundProject.ParentProjectId;
            }
            else
            {
                projectIdToSearch = null;
            }
        }

        return parentProjects.ToArray();
    }

    public async Task<bool> AddProjectAsync(Project? projectToAdd)
    {
        if (projectToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Projects?.Add(projectToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteProjectAsync(Project projectToDelete)
    {
        if (projectToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(projectToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }
}