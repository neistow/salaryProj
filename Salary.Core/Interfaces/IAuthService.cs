using System.Threading.Tasks;
using Salary.Core.Common;
using Salary.Core.DTO;

namespace Salary.Core.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserDto>> Register(RegisterDto registerDto);
        Task<Result<UserAuthenticatedDto>> Login(LoginDto loginDto);
    }
}