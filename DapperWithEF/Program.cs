using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using DataAccess.EntityFramework.Contexts;
using DataAccess.Extensions;



var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.InitDatabaseConnection<ProjectDbContext>(db =>
{
    db.AddDataAccessor();
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
