using Remind.Core.Models;

namespace Remind.Core.Services;

public interface IAuthService
{
    Task<(int, string)> Registration(RegistrationModel model, string role);
    Task<(int, string, DateTime?, ApplicationUser?)> Login(LoginModel model);
}