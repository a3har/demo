using CareerPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Services.IServices
{
    public interface IAuthService
    {
        public Task<APIResponse<string>> Login(string username, string password);

    }
}
