using MudBlazor;
using tictacApp.Interfaces;
using tictacApp.Services;

namespace tictacApp.Helpers;

public class BreadcrumbHelper
{
    public static List<BreadcrumbItem> BuildBreadcrumb(string rootName, string selectorUri, KeyValuePair<int, string?>[] items)
    {
        List<BreadcrumbItem> path = new List<BreadcrumbItem>();        

        BreadcrumbItem root = new BreadcrumbItem(rootName, href: $"/{selectorUri}", icon: AppIcons.Project);
        path.Add(root);

        foreach (KeyValuePair<int, string?> item in items)
        {
            BreadcrumbItem bcItem = new BreadcrumbItem(item.Value, href: $"/{selectorUri}/{item.Key}");
            path.Add(bcItem);
        }

        return path;
    }

    public static async Task<string> BuildSimpifiedBreadcrumb<T>(GenericCRUDServiceWithParents<T> crud, int id) where T: class, IIdLabel, IParent
    {
        KeyValuePair<int, string?>[] parents = await crud.GetParentsAsync(id);
        string asText = string.Empty;
        foreach (var pair in parents)
        {
            asText += (asText.Length == 0 ? "" : " / ") + pair.Value;
        }

        return asText;
    }

    public static string FormatStringForBreadcrumb(string str)
    {
        if (str.Length > 15)
        {
            return $"{str.Substring(0, 15)}...";
        }

        return str;
    }
}