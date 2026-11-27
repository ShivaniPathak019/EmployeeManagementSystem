using System.Reflection;
using EmployeeManagement.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManagement.Application.Services;

namespace EmployeeManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, Assembly? assembly = null)
    {
        if (assembly == null) assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddScoped<DepartmentService>();
        services.AddScoped<DesignationService>();
        services.AddScoped<EmployeeService>();
        services.AddScoped<AuthService>();
        return services;
    }
}