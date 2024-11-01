﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Remind.Core.Models;
using Remind.Core.Services;

namespace Remind.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<(int, string)> Registration(RegistrationModel model, string role)
    {
        var userExists = await userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
            return (0, "User already exists");

        ApplicationUser user = new()
        {
            Email = model.Email,
            UserName = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = model.FirstName,
            LastName = model.LastName,
        };
        var createUserResult = await userManager.CreateAsync(user, model.Password);
        if (!createUserResult.Succeeded)
            return (0, createUserResult.Errors.First().Description);

        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

        if (await roleManager.RoleExistsAsync(role))
            await userManager.AddToRoleAsync(user, role);

        return (1, "User created successfully!");
    }

    public async Task<(int, string, DateTime?, ApplicationUser?)> Login(LoginModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email.Trim());
        if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            return (0, "Invalid username/password combination", null, null);

        var userRoles = await userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var (token, expiryTime) = GenerateToken(authClaims);
        return (1, token, expiryTime, user);
    }

    public async Task<ApplicationUser> getUser(String email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user;
    }

    private (string, DateTime) GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
        var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWTKey:ValidIssuer"],
            Audience = _configuration["JWTKey:ValidAudience"],
            Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
            //Expires = DateTime.UtcNow.AddMinutes(1), // todo: add a realistic login time
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), token.ValidTo);
    }
}