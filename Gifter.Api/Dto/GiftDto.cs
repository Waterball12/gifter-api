using Gifter.Domain.Models;
using System.Collections.Generic;

namespace Gifter.Api.Dto
{
    public record GiftDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<GiftItem> Items { get; set; }

        public bool Multiple { get; set; }

        public string ShareLink { get; set; }

        public int Consumed { get; set; }

        public int MaxOpening { get; set; }

        public string UserId { get; set; }
    }
}
