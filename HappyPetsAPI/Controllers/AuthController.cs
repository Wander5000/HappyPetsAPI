using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Services.Auth;
using HappyPetsAPI.DTOs.Auth;

namespace HappyPetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Log-In")]
        public async Task<ActionResult> Login(LoginDTO request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("Sign-Up")]
        public async Task<ActionResult> Register(RegisterDTO request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }   
    }
}
