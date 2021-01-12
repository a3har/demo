using CareerPortal.Models;
using CareerPortal.Models.API.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Services.IServices
{
    public interface ICareerPortalService
    {

        public Task<APIResponse<User>> GetUser(string email);
        public Task<APIResponse<User>> RegisterUser(User user);
        public Task<APIResponse<IEnumerable<Experience>>> GetExperience(int id);
        public Task<APIResponse<IEnumerable<Education>>> GetEducation(int id);

    }
}
