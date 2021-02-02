using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Salary.Api.Extensions;
using Salary.Core.DTO;
using Salary.Core.Interfaces;

namespace Salary.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.Register(dto);

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.Login(dto);

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }
    }
}