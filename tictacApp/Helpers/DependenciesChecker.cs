using tictacApp.Data;
using tictacApp.Services;

namespace tictacApp.Helpers;

public class DependenciesChecker
{
    public bool AreDependenciesOk { get; set; } = true;

    private CommonService _service;

    private List<Tuple<string, string>> deps = new List<Tuple<string, string>>();
    

    public DependenciesChecker(CommonService service)
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

    private async Task CheckDependency<T>(string element, string link) where T: class
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