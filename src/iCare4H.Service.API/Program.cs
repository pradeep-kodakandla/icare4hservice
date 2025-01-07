using Swashbuckle.AspNetCore.Filters;
using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Application;
using iCare4H.Service.Infrastructure.Repository;
using iCare4H.DataAccess;
using iCare4H.DataAccess.Impl.Postgres;
using iCare4H.Service.Domain.Model.AppSettings;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.Configure<DatabaseConnection>(config.GetSection(nameof(DatabaseConnection)));
services.AddControllers();

services.AddEndpointsApiExplorer();

//Add Swagger bearer token authentication 
services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

services.AddAuthorization();

// create instance of data layer
var dbconfig = config.GetSection(nameof(DatabaseConnection));
var dataLayer = new PostgresDataLayer(dbconfig[nameof(DatabaseConnection.ServerName)],
                        dbconfig[nameof(DatabaseConnection.Database)],
                        dbconfig[nameof(DatabaseConnection.UserName)],
                        dbconfig[nameof(DatabaseConnection.Password)],
                        Convert.ToInt32(dbconfig[nameof(DatabaseConnection.Port)]));

//add internal repositories
services.AddSingleton<IAbstractDataLayer>(dataLayer);
services.AddScoped<IConfigRepository, ConfigRepository>();
services.AddScoped<IUserRepository, UserRepository>();

// add internal services
services.AddScoped<IConfigService, ConfigService>();
services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();