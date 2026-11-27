using System.Text;
using System.Text.Json.Serialization;
using EmployeeManagement.Application;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Identity;
using EmployeeManagement.WebAPI.Middleware;
using EmployeeManagement.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ----- Service registration -----
// Passing the Application assembly ensures MediatR and validators are discovered
// regardless of whether the app runs from the WebAPI project or a test host.
builder.Services.AddApplication(typeof(EmployeeManagement.Application.DependencyInjection).Assembly);
builder.Services.AddInfrastructure(builder.Configuration); //registerinbg infra layer 
builder.Services.AddHttpContextAccessor();   // this lets any class reach the current http request
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>(); //registering current user dervice

// getting values from the appsettings.json for checking purpose later 
var jwtKey = builder.Configuration["Jwt:Key"] ?? string.Empty;
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? string.Empty;

// registering the authentication  services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // if no api enpdoint given be it  a default ones
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// ----------adding controllers service-----------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// ----- HTTP pipeline -----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//middleware pipeline
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.Services.SeedAdminAsync(); // call  goes to Infra/Identity/IdentitySeeder.cs

app.Run();