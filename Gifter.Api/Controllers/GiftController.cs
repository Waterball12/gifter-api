using Gifter.Api.Dto;
using Gifter.Domain.Models;
using Gifter.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gifter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftRepository _repository;

        public GiftController(IGiftRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("open")]
        [AllowAnonymous]
        public async Task<ActionResult<OpenGiftDto>> OpenGiftAsync([FromQuery] string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var gift = await _repository.GetGiftAsync(id, cancellationToken);

            var userId = GetUserId(HttpContext);

            if (userId != null && gift.UserId == userId) return Unauthorized();

            if (gift == null)
                return NotFound();

            if (!gift.Multiple && gift.Consumed >= 1)
                return BadRequest("This gift has already been open");

            gift.Consumed += 1;
            await _repository.UpdateGiftAsync(gift, cancellationToken);

            return new OpenGiftDto()
            {
                Id = gift.Id,
                Items = gift.Items.ToList(),
                Name = gift.Name
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GiftDto>>> GetGiftAsync(CancellationToken cancellationToken)
        {
            var userId = GetUserId(HttpContext);

            if (userId == null) return Unauthorized();

            var gifts = await _repository.GetGiftsByUserId(userId, cancellationToken);

            var dto = gifts.Select(gift => new GiftDto()
            {
                Id = gift.Id,
                Name = gift.Name,
                Items = gift.Items.ToList(),
                Multiple = gift.Multiple,
                Consumed = gift.Consumed,
                MaxOpening = gift.MaxOpening,
                UserId = userId
            });

            return dto.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Gift>> CreateSharingGift([FromBody] CreateGiftDto dto, CancellationToken cancellationToken)
        {
            var userId = GetUserId(HttpContext);

            if (userId == null) return Unauthorized();

            var gift = new Gift()
            {
                Name = dto.Name,
                Items = dto.Items.ToHashSet(),
                Multiple = dto.Multiple,
                Consumed = 0,
                MaxOpening = dto.MaxOpening,
                UserId = userId
            };

            var result = await _repository.CreateGiftAsync(gift, cancellationToken);
            return result;
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Gift>> UpdateGiftAsync([FromBody] UpdateGiftDto dto, CancellationToken cancellationToken)
        {
            var userId = GetUserId(HttpContext);

            if (userId == null) return Unauthorized();

            var gift = await _repository.GetGiftByUserIdAsync(dto.Id, userId, cancellationToken);

            if (gift == null) return NotFound();

            gift.Name = dto.Name;
            gift.Multiple = dto.Multiple;

            await _repository.UpdateGiftAsync(gift, cancellationToken: cancellationToken);

            return gift;
        }
        
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Gift>> RemoveGiftAsync([FromBody] Gift entry, CancellationToken cancellationToken)
        {
            var userId = GetUserId(HttpContext);

            if (userId == null) return Unauthorized();

            var gift = await _repository.GetGiftByUserIdAsync(entry.Id, userId, cancellationToken);

            if (gift == null) return NotFound();

            await _repository.DeleteGiftAsync(gift.Id, cancellationToken: cancellationToken);

            return gift;
        }

        private string? GetUserId(HttpContext httpContext)
        {
            var user = httpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");

            return user != null ? user.Value : null;
        }
    }
}
