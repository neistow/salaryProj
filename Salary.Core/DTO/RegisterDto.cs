﻿namespace Salary.Core.DTO
{
    public record RegisterDto
    {
        public string Email { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}