namespace Salary.Core.Configurations
{
    public record JwtTokenConfig
    {
        public string Secret { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int AccessTokenExpirationMinutes { get; init; }
    }
}