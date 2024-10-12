using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Net.Http.Headers;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Company> companies = _unitOfWork.Company.GetAll().ToList();

            return View(companies);
        }
        public IActionResult Upsert(int? id)
        {

       
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company Company = _unitOfWork.Company.Get(u => u.Id == id);
                return View(Company);
            }
        }
        [HttpPost]
     
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
             
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);


                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
               

                return View(company);

            }
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Company> Companyies = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = Companyies });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });


        }
        #endregion

    }
}

