using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Salary.Core;
using Salary.Core.Common;
using Salary.Core.DTO;
using Salary.Core.Entities.Identity;
using Salary.Core.Interfaces;

namespace Salary.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<Result<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new User {UserName = registerDto.UserName, Email = registerDto.Email};
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result<UserDto>.Failure("", error.Description);
            }

            var dto = new UserDto {Email = user.Email, UserName = user.UserName};
            return Result<UserDto>.Success(dto);
        }

        public async Task<Result<UserAuthenticatedDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Result<UserAuthenticatedDto>.Failure(nameof(loginDto.Email), "Incorrect email");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Result<UserAuthenticatedDto>.Failure(nameof(loginDto.Password), "Incorrect password");
            }

            var dto = new UserAuthenticatedDto
            {
                User = user.MapToDto(),
                JwtToken = _jwtService.GenerateToken(user)
            };
            return Result<UserAuthenticatedDto>.Success(dto);
        }
    }
}