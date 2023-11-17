using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QLSV.Helpper;
using QLSV.Models;
using Microsoft.AspNetCore.Http;
using QLSV.Extension;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminHocSinhsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GameStoreDbContext _context;
        public INotyfService _notyfService { get; }
        public AdminHocSinhsController(IUnitOfWork unitOfWork, INotyfService notyfService,GameStoreDbContext context)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _context = context;
        }

        // GET: Admin/AdminHocSinhs
        public IActionResult Index()
        {
            var collection = _context.HocSinhs.Select(t => t).ToList();
            return View(collection);
        }

        // GET: Admin/AdminHocSinhs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var HocSinh = _context.HocSinhs.Find(id);
            if (HocSinh == null)
            {
                return NotFound();
            }

            return View(HocSinh);
        }

        // GET: Admin/AdminHocSinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminHocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gmail,Password,HoTen,date_of_birth,Salt,Balance,IsActive")] HocSinh hocsinh)
        {
            if (ModelState.IsValid)
            {
                //Xu ly Image
                try
                {
                    string salt = Utilities.GetRandomKey();
                    hocsinh.Password = (hocsinh.Password + salt.Trim()).ToMD5();
                    hocsinh.Salt = salt;
                    _context.HocSinhs.Add(hocsinh);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Create Success");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/AdminHocSinhs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var HocSinh = _context.HocSinhs.Find(id);
            if (HocSinh == null)
            {
                return NotFound();
            }
            return View(HocSinh);
        }

        // POST: Admin/AdminHocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Gmail,Password,HoTen,date_of_birth,Salt,Balance,IsActive")] HocSinh hocsinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.HocSinhs.Update(hocsinh);
                    _context.SaveChanges();
                    _notyfService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocSinhExists(hocsinh.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/AdminHocSinhs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var HocSinh = _context.HocSinhs.Find(id);
            if (HocSinh == null)
            {
                return NotFound();
            }

            return View(HocSinh);
        }

        // POST: Admin/AdminHocSinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var HocSinh = _context.HocSinhs.Find(id);
                _context.HocSinhs.Remove(HocSinh);
                _context.SaveChanges();
                _notyfService.Success("Xóa thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private bool HocSinhExists(int id)
        {
            return _unitOfWork.HocSinhRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
