using System.IdentityModel.Tokens.Jwt;//JwtSecurityToekn, JwtSecurityToeknHandler, JwtRegisteredClaimNames
using System.Security.Claims; //Claim , ClaimTypes
using System.Text; //Encoding , Guid , DateTimje, Convert
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; //SymmetricSecurityKey . SignningCredentials. SecurityAlgorithms

namespace EmployeeManagement.Infrastructure.Identity;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime ExpiresAt) Generate(User user, int? employeeId)
    {
        var key = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        if (string.IsNullOrWhiteSpace(key) || key.Length < 32)
            throw new InvalidOperationException("Jwt:Key must be configured with at least 32 characters.");

        if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
            throw new InvalidOperationException("Jwt:Issuer and Jwt:Audience must be configured.");
        
        //calculating the expiry time of token 
        var expiresInMinutes = Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"] ?? "60");
        var expiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes);

        // token contains these things 
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()), // target-typed 
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // a random unique id for this token using guid
        };

        if (employeeId.HasValue)
            claims.Add(new Claim("employeeId", employeeId.Value.ToString()));

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), //wrapping the ssecret key into cryptographic key 
            SecurityAlgorithms.HmacSha256); //algorithm used is hmaSha256
        //final token 
        var token = new JwtSecurityToken(
            issuer: issuer, // named arguments
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt,
            signingCredentials: credentials);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}
