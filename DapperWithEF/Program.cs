using DataAccess.EntityFramework.Contexts;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = configuration["IdentityServerURL"];
                        options.RequireHttpsMetadata = false;
                        options.Audience = "catalog_service_api";
                    });

services.AddAuthorization(options =>
{
    options.AddPolicy("read",
        policy => policy.RequireClaim("scope", "catalog.api.read", "catalog.api.full"));
    options.AddPolicy("write",
        policy => policy.RequireClaim("scope", "catalog.api.write", "catalog.api.full"));
});

services.AddControllers();

// If you want to use another DbContext, change the ProjectDbContext below. 

services.InitDatabaseConnection<PostgreSqlDbContext>(db =>
{
    db.Configuration = builder.Configuration;
    db.AddDataAccessor(); // Default is Dapper.
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
