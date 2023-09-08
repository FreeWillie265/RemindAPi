using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Remind.Core.Models;
using Remind.Core.Services;
using RemindAPi.Resources;

namespace RemindAPi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IMapper _mapper;

    public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, IMapper mapper)
    {
        _authService = authService;
        _logger = logger;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            var (status, token, expiryTime, user) = await _authService.Login(model);
            if (status == 0)
                return Unauthorized(token);
            var userResource = _mapper.Map<ApplicationUser, UserResource>(user);
            return Ok(new
            {
                token,
                expiration = expiryTime,
                user = userResource
            });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Register([FromBody] RegistrationModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            var (status, message) = await _authService.Registration(model, UserRoles.Admin);
            if (status == 0)
            {
                return BadRequest(message);
            }

            var user = _mapper.Map<ApplicationUser, UserResource>(await _authService.getUser(model.Email));
            return CreatedAtAction(nameof(Register), user);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
}