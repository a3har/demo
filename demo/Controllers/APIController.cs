using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using CareerPortal.Models.API;
using CareerPortal.Models.API.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Controllers
{
    [ApiController]
    [Route("api")]
    public class APIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public APIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Education/{id}")]
        public async Task<IActionResult> GetEducation(int id)
        {
            var objFromDb = _unitOfWork.Education.GetAll(c => c.UserId == id);
            APIResponse<IEnumerable<Education>> response = new APIResponse<IEnumerable<Education>>()
            {
                Data = objFromDb
            };
            if (objFromDb.Count() == 0)
            {
                response.Success = false;
                response.Message = "Educations not found for user " + id.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpGet("Experience/{id}")]
        public async Task<IActionResult> GetExperience(int id)
        {
            var objFromDb = _unitOfWork.Experience.GetAll(c => c.UserId == id);
            APIResponse<IEnumerable<Experience>> response = new APIResponse<IEnumerable<Experience>>()
            {
                Data = objFromDb
            };
            if (objFromDb.Count() == 0)
            {
                response.Success = false;
                response.Message = "Experience not found for user " + id.ToString();
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("User")]
        public async Task<IActionResult> GetUserInfo(GetUserDto getUserDto)
        {
            var objFromDb = _unitOfWork.User.GetFirstOrDefault(c => c.Email.Equals(getUserDto.Email.ToLower()));
            APIResponse<User> response = new APIResponse<User>()
            {
                Data = objFromDb
            };
            if (objFromDb == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return NotFound(response);
            }
            return Ok(response);
        }



    }
}
