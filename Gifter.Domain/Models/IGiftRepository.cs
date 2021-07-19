using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#nullable enable

namespace Gifter.Domain.Models
{
    public interface IGiftRepository
    {
        Task<Gift?> CreateGiftAsync(Gift gift, CancellationToken cancellationToken);

        Task<Gift?> GetGiftAsync(string giftId, CancellationToken cancellationToken);

        Task<IEnumerable<Gift>> GetGiftsByUserId(string userId, CancellationToken cancellationToken = default);

        Task<Gift> UpdateGiftAsync(Gift gift, CancellationToken cancellationToken = default);

        Task<Gift> GetGiftByUserIdAsync(string giftId, string userId, CancellationToken cancellationToken = default);

        Task DeleteGiftAsync(string giftId, CancellationToken cancellationToken = default);
    }
}
