using MudBlazor;
using tictacApp.Interfaces;

namespace tictacApp.Helpers;

public class BreadcrumbHelper
{
    public static List<BreadcrumbItem> BuildBreadcrumb(string rootName, string rootUri, KeyValuePair<int, string?>[] items)
    {
        List<BreadcrumbItem> path = new List<BreadcrumbItem>();        

        BreadcrumbItem root = new BreadcrumbItem(rootName, href: $"/{rootUri}", icon: AppIcons.Project);
        path.Add(root);

        foreach (KeyValuePair<int, string?> item in items)
        {
            string asText = FormatStringForBreadcrumb(item.Value);
            BreadcrumbItem bcItem = new BreadcrumbItem(asText, href: $"/{rootUri}/{item.Key}");
            path.Add(bcItem);
        }

        return path;
    }

    public static async Task<string> BuildSimpifiedBreadcrumb<T>(IGenericCRUDServiceWithParents crud, int id) where T: class, IIdLabel, IParent
    {
        int[] ids = { id };

        Dictionary<int, string> results = await BuildSimpifiedBreadcrumb<T>(crud, ids, false);
        
        return results.ContainsKey(id) ? results[id] : string.Empty;
    }

    public static async Task<Dictionary<int, string>> BuildSimpifiedBreadcrumb<T>(IGenericCRUDServiceWithParents crud, int[] ids, bool noLastLevel) where T: class, IIdLabel, IParent
    {
        Dictionary<int, string> allSimplifedBreadcrumbs = new Dictionary<int, string>();
        
        Dictionary<int, KeyValuePair<int, string?>[]> allParents = await crud.GetParentsAsync<T>(ids);

        //either we want to display the full path of an element (like in the characteristics selector) or 
        //we want to display the path up to the element like in the activities at risk
        int stopOffset = noLastLevel ? 1 : 0;      

        foreach (var parentsOfOne in allParents)
        {
            string asText = string.Empty;

            for (int i = 0; i < parentsOfOne.Value.Count() - stopOffset; i++)
            {
                var pair = parentsOfOne.Value[i];
                asText += (asText.Length == 0 ? "" : " / ") + FormatStringForBreadcrumb(pair.Value);
            }

            if (noLastLevel)
            {
                asText += " / ";
            }

            allSimplifedBreadcrumbs.Add(parentsOfOne.Key, asText);
        }

        return allSimplifedBreadcrumbs;
    }

    public static string FormatStringForBreadcrumb(string? str)
    {
        if (str != null && str.Length > Constants.MaxBreadcrumReadeableItemTextLength)
        {
            return $"{str.Substring(0, Constants.MaxBreadcrumReadeableItemTextLength)}...";
        }

        return str ?? string.Empty;
    }
}