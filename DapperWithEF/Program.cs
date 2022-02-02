using DataAccess.EntityFramework.Contexts;
using DataAccess.Extensions;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;


services.AddControllers();

// Change the ProjectDbContext below, if you want to use another DbContext. 

services.InitDatabaseConnection<ProjectDbContext>(db =>
{
    db.Configuration = builder.Configuration;
    db.AddDataAccessor();
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
