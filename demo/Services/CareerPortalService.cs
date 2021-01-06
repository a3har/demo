using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using demo.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Services
{
    public class CareerPortalService : ICareerPortalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CareerPortalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<IEnumerable<Education>>> GetEducation(int id)
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
            }
            return response;
        }

        public async Task<APIResponse<IEnumerable<Experience>>> GetExperience(int id)
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
                return response;
            }
            return response;
        }

        public async Task<APIResponse<User>> GetUser(string email)
        {
            var objFromDb = _unitOfWork.User.GetFirstOrDefault(c => c.Email.Equals(email.ToLower()));
            APIResponse<User> response = new APIResponse<User>()
            {
                Data = objFromDb
            };
            if (objFromDb == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }
            return response;
        }

        public async Task<APIResponse<User>> RegisterUser(User user)
        {
            APIResponse<User> response = new APIResponse<User>()
            {
                Success = false
            };
            try
            {
                   var ObjFromDb = _unitOfWork.User.GetFirstOrDefault(i => i.Email == user.Email);

                    if (ObjFromDb == null)
                    {
                        _unitOfWork.User.Add(user);
                        _unitOfWork.Save();

                        response.Success = true;
                        response.Message = "User successfully added";
                        response.Data = _unitOfWork.User.GetFirstOrDefault(c => c.Email.ToLower().Equals(user.Email.ToLower()));

                        return response;

                    }

                    response.Message = "Email ID already exists";
                    return response;
            }


            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
