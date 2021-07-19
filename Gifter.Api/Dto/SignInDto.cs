namespace Gifter.Api.Dto
{
    public record SignInDto
    {
        public string Username { get; init; }
    }

    public record UserAuth
    {
        public string Username { get; init; }
        public string Token { get; init; }
    }
}
