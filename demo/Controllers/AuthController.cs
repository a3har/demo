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
        private readonly IAuthService _authservice;

        public AuthController(IAuthService service)
        {
            _authservice = service;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            APIResponse<string> response = await _authservice.Login(
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
