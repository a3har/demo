using CareerPortal.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult IsEmailInUse(string email)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(i => i.Email == email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [HttpGet]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult DoesEmailExist(string email)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(i => i.Email == email);

            if (user == null)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
