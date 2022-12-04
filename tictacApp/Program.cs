using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using tictacApp.Services;
using tictacApp.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
///MudBlazor
builder.Services.AddMudServices();       

///efcore specitic initialization for a blazor server app
builder.Services.AddDbContextFactory<TictacDBContext>(opt => opt.UseSqlite(@"Data Source=tictacDB.db"));
builder.Services.AddDbContext<TictacDBContext>(opt => opt.UseSqlite(@"Data Source=tictacDB.db"));

builder.Services.AddSingleton<TimeLogsService>();

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
