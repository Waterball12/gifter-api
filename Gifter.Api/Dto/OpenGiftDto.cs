using Gifter.Domain.Models;
using System.Collections.Generic;

namespace Gifter.Api.Dto
{
    public record OpenGiftDto
    {
        public string Id { get; init; }

        public IEnumerable<GiftItem> Items { get; init; }

        public string Name { get; init; }
    }
}
