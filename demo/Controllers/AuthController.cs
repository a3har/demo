using CareerPortal.Models;
using CareerPortal.Models.API.DTO;
using demo.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ICareerPortalService _service;

        public AuthController(ICareerPortalService service)
        {
            _service = service;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            APIResponse<string> response = await _service.Login(
                request.Email, request.Password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
