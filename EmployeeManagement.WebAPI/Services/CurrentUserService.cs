using System.Security.Claims;
using EmployeeManagement.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.WebAPI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? EmployeeId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirst("employeeId")?.Value;
            return int.TryParse(value, out var id) ? id : null;
        }
    }

    public bool IsAdmin => _httpContextAccessor.HttpContext?.User.IsInRole("Admin") ?? false;

    public string? Email => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
}
