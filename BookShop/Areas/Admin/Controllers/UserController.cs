using BookShop.DataAccess.Data;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Models.ViewModels;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController( IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
          return View();
        }
        public IActionResult RoleManagment(string userId)
        {
            var userFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties:"Company");

                RoleManagmentVM RoleVM = new() { 
                ApplicationUser = userFromDb,
                RoleList = _roleManager.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            RoleVM.ApplicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userId))
                .GetAwaiter().GetResult().FirstOrDefault();
            return View(RoleVM);
        }
        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVM)
        {
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser
                  .Get(u => u.Id == roleManagmentVM.ApplicationUser.Id);
            string oldRole = _userManager.GetRolesAsync(applicationUser).GetAwaiter().GetResult().FirstOrDefault();

            //check if the input role equal the role in the database
            if (!(roleManagmentVM.ApplicationUser.Role == oldRole))
            {
                if(roleManagmentVM.ApplicationUser.Role == SD.Role_Company)
                {
                    applicationUser.CompanyId = roleManagmentVM.ApplicationUser.CompanyId;
                }
                if(oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagmentVM.ApplicationUser.Role).GetAwaiter().GetResult();
                TempData["success"]= "Operation Successful";
            }
            else
            {
                if(oldRole==SD.Role_Company && applicationUser.CompanyId != roleManagmentVM.ApplicationUser.CompanyId)
                {
                    applicationUser.CompanyId = roleManagmentVM.ApplicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                    _unitOfWork.Save();
                    TempData["success"] = "Operation Successful";
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ApplicationUser> users = _unitOfWork.ApplicationUser.GetAll(includeProperties:"Company").ToList();

          
            foreach (var user in users)
            {
                // get the roleId from the userRole table based on the userId
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
                if(user.Company == null)
                {
                    user.Company = new() { Name = "" };
                }
            }
            return Json(new { data = users });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
         if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/UnLocking" });

            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd> DateTime.Now)
            //user is currently locked
            {
                objFromDb.LockoutEnd = DateTime.Now;
                //unlock
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation Successful" });


        }

        #endregion

    }
}

