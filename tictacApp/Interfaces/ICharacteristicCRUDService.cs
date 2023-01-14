using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface ICharacteristicCRUDService: IGenericCRUDServiceWithParents
{
    Task<Characteristic[]> GetAllAsync(int? parentId, bool displayGradeAndGroup, bool showClosedOnly);

    Task<IEnumerable<int>> GetIdOfCharacteristicsWithChildren(int[] characteristicsIds);
}