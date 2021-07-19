namespace Gifter.Domain.Options
{
    public record TokenOptions
    {
        public string Audience { get; init; }
        public string Issuer { get; init; }
        public string Secret { get; init; }
    }
}
