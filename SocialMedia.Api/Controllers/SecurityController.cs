using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enum;
using SocialMedia.Core.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.ADMIN))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            _securityService = securityService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(SecurityDto _object)
        {
            var newSecurity = _mapper.Map<Security>(_object);
            await _securityService.RegisterUser(newSecurity);

            var result = _mapper.Map<SecurityDto>(newSecurity);
            var response = new ApiResponse<SecurityDto>(result);

            return Ok(response);
        }
    }
}
