using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public TokenController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin userLogin)
        {
            if(IsValidUser(userLogin))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }

            return NotFound();
        }

        private bool IsValidUser(UserLogin user)
        {
            return true;
        }
        private string GenerateToken()
        {
            // generate header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // generate claims
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "Carlos Diaz"),
                new Claim(ClaimTypes.Email, "carlos.cedp@gmail.com"),
                new Claim(ClaimTypes.Role, "ADMIN")
            };

            // generate payload
            var payload = new JwtPayload(
                Configuration["Authentication:Issuer"], 
                Configuration["Authentication:Audience"], 
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)
                );

            // generate token

            var token = new JwtSecurityToken(header, payload);
            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return generatedToken;
        }
    }
}
