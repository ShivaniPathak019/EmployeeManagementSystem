using EmployeeManagement.Application.Auth.Commands.Login;
using EmployeeManagement.Application.Auth.Commands.Register;
using EmployeeManagement.Application.Auth.Dtos;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Application.Common.Interfaces.Repositories;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IEmployeeRepository employeeRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (await _userRepository.EmailExistsAsync(email, cancellationToken))
            throw new ConflictException($"User with email '{email}' already exists.");

        var user = new User
        {
            Email = email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Role = "Employee",
            IsActive = true
        };

        var created = await _userRepository.AddAsync(user, cancellationToken);

        var employee = await _employeeRepository.GetByEmailAsync(email, cancellationToken);
        var employeeId = employee?.Id;

        if (employee is not null)
        {
            created.EmployeeId = employee.Id;
            await _userRepository.UpdateAsync(created, cancellationToken);
        }

        return ToResponse(created, employeeId);
    }

    public async Task<AuthResponseDto> LoginAsync(
        LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null || !user.IsActive ||
            !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");

        int? employeeId = null;

        if (user.Role == "Employee")
        {
            var employee = user.EmployeeId.HasValue
                ? await _employeeRepository.GetByIdAsync(user.EmployeeId.Value, cancellationToken)
                : await _employeeRepository.GetByEmailAsync(email, cancellationToken);

            if (employee is null || !employee.IsActive)
                throw new UnauthorizedException("No active employee record is linked to this account.");

            employeeId = employee.Id;

            if (user.EmployeeId != employee.Id)
            {
                user.EmployeeId = employee.Id;
                await _userRepository.UpdateAsync(user, cancellationToken);
            }
        }

        return ToResponse(user, employeeId);
    }

    private AuthResponseDto ToResponse(User user, int? employeeId)
    {
        var (token, expiresAt) = _tokenGenerator.Generate(user, employeeId);

        return new AuthResponseDto
        {
            Token = token,
            ExpiresAt = expiresAt,
            Email = user.Email,
            Role = user.Role
        };
    }
}
