using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyBuilding.Security.Interfaces;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPropertyBuilding.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public AuthenticationController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [HttpPost]
        [Route("Authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] User credentials)
        {
            var token = jwtAuthenticationManager.Authenticate(credentials);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
