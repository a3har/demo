using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using CareerPortal.Models.API;
using CareerPortal.Models.API.DTO;
using demo.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Controllers
{
    [ApiController]
    [Authorize]
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


        [HttpPost("User/Register")]
        public async Task<IActionResult> RegisterUser(User user)
        {
            APIResponse<User> response = new APIResponse<User>();
            try {
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

        [HttpPut("User/Update")]
        public async Task<IActionResult> UpdateUser(PutUserDto putUserDto)
        {
            APIResponse<User> response = new APIResponse<User>();
            try
            {
                  var ObjFromDb = _unitOfWork.User.GetFirstOrDefault(i => i.Email == putUserDto.Email);

                    if (ObjFromDb == null)
                    {
                        response.Message = "Email ID does not exist";
                        response.Success = false;
                        return BadRequest(response);
                    }

                    ObjFromDb.PhoneNumber = putUserDto.PhoneNumber;
                    ObjFromDb.Address = putUserDto.Address;
                    ObjFromDb.DateOfBirth = putUserDto.DateOfBirth;
                    ObjFromDb.Name = putUserDto.Name;
                    
                    
                    _unitOfWork.User.Update(ObjFromDb);
                    _unitOfWork.Save();
                    response.Message = "User successfully updated";
                    response.Data = _unitOfWork.User.GetFirstOrDefault(c => c.Email.ToLower().Equals(putUserDto.Email.ToLower()));

                    return Ok(response);
               
            }


            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return BadRequest(response);
            }
        }


        


    }
}
