using Gifter.Api.Dto;
using Gifter.Domain.Options;
using Gifter.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Gifter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOptions<TokenOptions> _tokenOptions;

        public UserController(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<UserAuth> SignInAsync([FromBody] SignInDto signIn)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, signIn.Username),
                new ("UserId", Guid.NewGuid().ToString())
            };

            var token = TokenUtils.GetToken(claims, _tokenOptions.Value);

            return new UserAuth()
            {
                Username = signIn.Username,
                Token = token
            };
        }
    }
}
