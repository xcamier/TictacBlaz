using MudBlazor.Services;
using tictacApp.Services;
using tictacApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

///MudBlazor
builder.Services.AddMudServices();       
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});


///efcore specitic initialization for a blazor server app
builder.Services.AddDbContextFactory<TictacDBContext>(opt => opt.UseSqlite(@"Data Source=tictacDB.db"));
builder.Services.AddDbContext<TictacDBContext>(opt => opt.UseSqlite(@"Data Source=tictacDB.db"));

builder.Services.AddSingleton<TimeLogsService>();
builder.Services.AddSingleton<TagsService>();
builder.Services.AddSingleton<GradesService>();
builder.Services.AddSingleton<ProjectsService>();
builder.Services.AddSingleton<ObjectivesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
