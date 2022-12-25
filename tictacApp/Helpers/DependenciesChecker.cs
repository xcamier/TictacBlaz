using tictacApp.Data;
using tictacApp.Services;
using tictacApp.Interfaces;

namespace tictacApp.Helpers;

public class DependenciesChecker<T> where T: class, IId
{
    public bool AreDependenciesOk { get; set; } = true;

    private List<Tuple<string, string>> deps = new List<Tuple<string, string>>();
    
    private GenericCRUDService<T> _service;

    public DependenciesChecker(GenericCRUDService<T> service)
    {
        _service = service;
    }

    public Tuple<string, string>[] GetUnsatisfiedDependencies()
    {
        return deps.ToArray();
    }

    public async Task CheckGradesDependecy()
    {
        await CheckDependency<Grade>("Grades", "/grades");
    }

    public async Task CheckGroupOfCharacteristicssDependecy()
    {
        await CheckDependency<CharacteristicsGroup>("Groups of Characteristics", "/characteristicsGroups");
    }

    public async Task CheckActorDependency()
    {
        await CheckDependency<Actor>("Actors", "/actors");
    }

    private async Task CheckDependency<T>(string element, string link) where T: class, IId
    {
        bool isDepOk = await _service.HasAtLeastOneItem<T>();
        if (!isDepOk)
        {
            AddDependencyNotSatisfied(element, link);
            AreDependenciesOk = false;
        }
    }

    private void AddDependencyNotSatisfied(string element, string link)
    {
        Tuple<string, string> dep = new Tuple<string, string>(element,link);
        deps.Add(dep);
    }
}