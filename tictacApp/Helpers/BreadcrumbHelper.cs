using MudBlazor;

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

    public static string FormatStringForBreadcrumb(string str)
    {
        if (str.Length > 15)
        {
            return $"{str.Substring(0, 15)}...";
        }

        return str;
    }
}