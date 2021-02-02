using Salary.Core.Entities.Identity;

namespace Salary.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}