namespace Gifter.Api.Dto
{
    public record UpdateGiftDto
    {
        public string Id {get;init;}
        public string Name { get; init; }
        public bool Multiple { get; init; }
    }
}
