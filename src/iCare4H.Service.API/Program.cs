using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Application;
using iCare4H.Service.Infrastructure.Repository;
using iCare4H.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();

// 🔹 Register Authentication BEFORE `builder.Build()`
var key = Encoding.ASCII.GetBytes("bP3!x5$G8@r9ZyL2WqT4!bN7eK1sD#uV");// Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]); // Read key from appsettings.json
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

services.AddAuthorization();

// 🔹 Register Swagger with JWT Authentication Support
services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// 🔹 Register Data Layer (Fix for Dependency Injection)
services.AddScoped<IAbstractDataLayer, AbstractDataLayer>();

// 🔹 Register Repositories
services.AddScoped<IUserRepository, UserRepository>();

// 🔹 Register Services
services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Ensure JSON format is preserved
    });



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("*") // Add more origins if needed
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()); // Add this if using authentication (e.g., cookies, JWTs)
});

// 🔹 Build the app AFTER service registrations
var app = builder.Build();

// Apply CORS globally
app.UseCors("AllowAngularApp");

// 🔹 Enable Swagger UI in Development Mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Configure Middleware
app.UseHttpsRedirection();
app.UseAuthentication(); // ✅ Must be BEFORE Authorization
app.UseAuthorization();
// ✅ Ensure JSON responses are always returned
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json";
    await next();
});

app.MapControllers();

// 🔹 Run the App
app.Run();
