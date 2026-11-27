using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAdminAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var email = configuration["SeedAdmin:Email"];
        var password = configuration["SeedAdmin:Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return;

        email = email.Trim().ToLowerInvariant();

        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        if (await userRepository.EmailExistsAsync(email, CancellationToken.None))
            return;

        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        await userRepository.AddAsync(new User
        {
            Email = email,
            PasswordHash = hasher.Hash(password),
            Role = "Admin",
            IsActive = true
        }, CancellationToken.None);
    }
}
