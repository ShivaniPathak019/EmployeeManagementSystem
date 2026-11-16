using System.Text.Json.Serialization;
using EmployeeManagement.Application;
using EmployeeManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ----- Service registration -----
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----- HTTP pipeline -----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();