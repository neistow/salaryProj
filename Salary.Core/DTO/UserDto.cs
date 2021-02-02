namespace Salary.Core.DTO
{
    public record UserDto
    {
        public string UserName { get; init; }
        public string Email { get; init; }
    }
}