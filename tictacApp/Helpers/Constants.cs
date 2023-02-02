using MudBlazor;

namespace tictacApp.Helpers;

public static class Constants
{
    public const int Label1CharLength = 1;    
    public const int LabelMinLength = 3;
    public const int LabelStandardLength = 25;
    public const int LabelLongLength = 50;
    public const int LabelShortLength = 15;
    public const int DescriptionStandardLength = 255;
    public const int DescriptionFullLength = 510;
    public const int DescriptionMidLength = 140;
    public const int MaxBreadcrumReadeableItemTextLength = 20;
}

public static class AppIcons
{
    public const string Project = @Icons.Material.Filled.DeviceHub;
    public const string Edit = @Icons.Outlined.Edit;
    public const string Delete = @Icons.Outlined.Delete;
    public const string View = @Icons.Outlined.Pageview;
    public const string ViewWithContent = @Icons.Filled.Pageview;
    public const string Add = @Icons.Material.Filled.PostAdd;
    public const string Objective = @Icons.Outlined.Lightbulb;
    public const string Select = @Icons.Outlined.CheckBox;
    public const string Plus = @Icons.Material.Filled.Add;
    public const string Positive = @Icons.Filled.ThumbUp;
    public const string Negative =@Icons.Filled.ThumbDown;
    public const string Label = @Icons.Outlined.Label;
    public const string Characteristic = @Icons.Material.Filled.Anchor;
    public const string Calculate = @Icons.Material.Outlined.Calculate;
    public const string Comment = @Icons.Material.Filled.Comment;
    public const string List = @Icons.Material.Outlined.ListAlt;
    public const string AddLog = @Icons.Material.Filled.MoreTime;
    public const string QuickLog = @Icons.Material.Filled.AvTimer;
    public const string Login = @Icons.Material.Filled.Login;
    public const string Register = @Icons.Material.Filled.AppRegistration;
    public const string Duplicate = @Icons.Material.Filled.ContentCopy;
}

public static class ProgressStatus
{
    public const string OnTrack = "On Track";
    public const string Behind = "Behind";
}

public static class SettingsKeys
{
    public const string OrangeLow = "orange_low";
    public const string OrangeHigh = "orange_high";
    public const string GreenLow = "green_low";
    public const string GreenHigh = "green_high";
}