using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;
using tictacApp.Interfaces;

public class DefaultDataRetriversHelper
{
    public static async Task<TagView[]> GetTagsAsync(IGenericCRUDService tagsService, IMapper mapper)
    {
        Tag[] dbTags = await tagsService.GetAllAsync<Tag>();
        dbTags = dbTags.OrderBy(t => t.Label).ToArray();

        return mapper.Map<TagView[]>(dbTags);
    }
}