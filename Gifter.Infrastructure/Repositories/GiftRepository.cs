using Gifter.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gifter.Infrastructure.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private readonly GiftContext _context;

        public GiftRepository(GiftContext context)
        {
            _context = context;
        }

        public async Task<Gift> CreateGiftAsync(Gift gift, CancellationToken cancellationToken)
        {
            await _context.Gift.InsertOneAsync(gift, cancellationToken: cancellationToken);

            return gift;
        }

        public async Task DeleteGiftAsync(string giftId, CancellationToken cancellationToken = default)
        {
            await _context.Gift.DeleteOneAsync(x => x.Id == giftId, cancellationToken: cancellationToken);
        }

        public async Task<Gift?> GetGiftAsync(string giftId, CancellationToken cancellationToken)
        {
            return await _context.Gift.AsQueryable().FirstOrDefaultAsync(x => x.Id == giftId, cancellationToken);
        }

        public async Task<Gift> GetGiftByUserIdAsync(string giftId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Gift.AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == giftId && x.UserId == userId, cancellationToken);
        }

        public async Task<IEnumerable<Gift>> GetGiftsByUserId(string userId, CancellationToken cancellationToken = default)
        {
            var gifts = await _context.Gift.AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return gifts;
        }

        public async Task<Gift> UpdateGiftAsync(Gift gift, CancellationToken cancellationToken = default)
        {
            await _context.Gift.ReplaceOneAsync(x => x.Id == gift.Id, gift, cancellationToken: cancellationToken);

            return gift;
        }
    }
}
