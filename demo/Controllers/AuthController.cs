using CareerPortal.DataAccess.Repository.IRepository;
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
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IAuthService service,IUnitOfWork unitOfWork)
        {
            _authservice = service;
            _unitOfWork = unitOfWork;
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
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(User user)
        {
            APIResponse<User> response = new APIResponse<User>();
            try
            {
                if (ModelState.IsValid)
                {
                    var ObjFromDb = _unitOfWork.User.GetFirstOrDefault(i => i.Email == user.Email);

                    if (ObjFromDb == null)
                    {
                        _unitOfWork.User.Add(user);
                        _unitOfWork.Save();


                        response.Message = "User successfully added";
                        response.Data = _unitOfWork.User.GetFirstOrDefault(c => c.Email.ToLower().Equals(user.Email.ToLower()));

                        return Ok(response);

                    }

                    response.Message = "Email ID already exists";
                    response.Success = false;
                    return BadRequest(response);
                }
            }


            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return BadRequest(response);
            }
            response.Message = "Something went wrong";
            response.Success = false;
            return BadRequest(response);

        }
    }



}
