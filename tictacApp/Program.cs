using MudBlazor.Services;
using tictacApp.Services;
using tictacApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using tictacApp.Data;
using AutoMapper;
using tictacApp.ViewModels;
using tictacApp.Interfaces;

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
builder.Services.AddSingleton<ObservationsService>();
builder.Services.AddSingleton<IGenericCRUDService, GenericCRUDService>();
builder.Services.AddSingleton<IGenericCRUDServiceWithParents, GenericCRUDServiceWithParents>();
builder.Services.AddSingleton<IPlannedActivityCRUDService, PlannedActivityCRUDService>();
builder.Services.AddSingleton<ICharacteristicCRUDService, CharacteristicCRUDService>();

//TODO: to remove when stabilized
//builder.Services.AddSingleton<ItemSelectionService<Project, TimeLog>>();
//builder.Services.AddSingleton<ItemSelectionService<Objective, TimeLog>>();
//builder.Services.AddSingleton<ItemSelectionService<Characteristic, TimeLog>>();

builder.Services.AddSingleton<ItemSelectionService<CharacteristicView, TimeLogView>>();
builder.Services.AddSingleton<ItemSelectionService<CharacteristicView, ObservationView>>();

//check if those two are still usefull after replacement by...
builder.Services.AddSingleton<ItemSelectionService<ProjectView, TimeLogView>>();
builder.Services.AddSingleton<ItemSelectionService<ObjectiveView, TimeLogView>>();
//... this one
builder.Services.AddSingleton<ItemSelectionService<PlannedActivityView, TimeLogView>>();

builder.Services.AddSingleton<ItemSelectionService<Characteristic, Observation>>();

//Automapper
var mapperConfiguration = new MapperConfiguration(configuration =>
{
    configuration.AddProfile(new MappingTimelog());
    configuration.AddProfile(new MappingObservation());
    configuration.AddProfile(new MappingTag());
    configuration.AddProfile(new MappingActor());
    configuration.AddProfile(new MappingProject());
    configuration.AddProfile(new MappingObjective());
    configuration.AddProfile(new MappingChracteristic());
});

var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//runs the migration if needed
var serviceProvider = builder.Services.BuildServiceProvider();
IServiceScopeFactory? scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
MigrationService service = new MigrationService(scopeFactory);
await service.MigrateAsync();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
