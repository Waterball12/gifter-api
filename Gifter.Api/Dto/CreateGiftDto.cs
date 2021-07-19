using Gifter.Domain.Models;
using System.Collections.Generic;

namespace Gifter.Api.Dto
{
    public record CreateGiftDto
    {
        public string Name { get; set; }

        public IEnumerable<GiftItem> Items { get; set; }

        public bool Multiple { get; set; }

        public int MaxOpening { get; set; }
    }
}
