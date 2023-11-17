using AspNetCoreHero.ToastNotification.Abstractions;
using QLSV.Areas.Admin.Models;
using QLSV.Helpper;
using QLSV.Models;
using QLSV.ModelViews;
using QLSV.OtpModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QLAdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public QLAdminController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.AdminRepository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: AdminProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("TaiKhoan,Password,HoTen,IsActive")] QLSV.Models.Admin Admin)
        {
            if (id != Admin.TaiKhoan)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.AdminRepository.Update(Admin);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Update successful");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(Admin);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _unitOfWork.AdminRepository.GetById(id);
                _unitOfWork.AdminRepository.Delete(product);
                _unitOfWork.SaveChange();
                _notyfService.Success("Delete successful");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult Index()
        {
            var ls = _unitOfWork.AdminRepository.GetAll().ToList();
            return View(ls);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("TaiKhoan,Password,HoTen,IsActive")] QLSV.Models.Admin Admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Admin.TaiKhoan != null)
                    {
                        _unitOfWork.AdminRepository.Create(Admin);
                        _unitOfWork.SaveChange();
                        _notyfService.Success("Successfully added new");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _notyfService.Error("Error");
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(Admin);
        }
        public string Role
        {
            get
            {
                var gh = HttpContext.Session.GetString("Role");
                if (gh == null)
                {
                    gh = "";
                }
                return gh;
            }
        }
    }
}
