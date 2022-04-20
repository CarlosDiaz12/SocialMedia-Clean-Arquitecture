﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
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
    [Produces("application/json")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ISecurityService _securityService;
        public TokenController(IConfiguration configuration, ISecurityService securityService)
        {
            Configuration = configuration;
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin userLogin)
        {
            var validation = await IsValidUser(userLogin);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }

            return NotFound();
        }

        private async Task<(bool,Security)> IsValidUser(UserLogin login)
        {
            var user = await _securityService.GetLoginByCredentials(login);

            return (user != null, user);
        }
        private string GenerateToken(Security security)
        {
            // generate header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // generate claims
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, security.UserName),
                new Claim("User", security.User),
                new Claim(ClaimTypes.Role, security.Role.ToString())
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
