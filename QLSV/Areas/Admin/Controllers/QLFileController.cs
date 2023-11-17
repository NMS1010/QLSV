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
using QLSV.Helper;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class QLFileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GameStoreDbContext _context;
        public INotyfService _notyfService { get; }
        public QLFileController(IUnitOfWork unitOfWork, INotyfService notyfService, GameStoreDbContext context)
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
        // GET: Admin/AdminHocSinhs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var HocSinh = _context.HocSinhs.Find(id);
            HttpContext.Session.SetString("HocSinhId", id.ToString());
            if (HocSinh == null)
            {
                return NotFound();
            }
            return View();
        }
        public static async Task<string> taifile(Microsoft.AspNetCore.Http.IFormFile file)
        {
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(file.FileName);
                FileName = file.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
                var _GetFilePath = Common.GetFilePath(FileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(_FileStream);
                }
                return FileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Admin/AdminHocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("name")] QLSV.Models.File hocsinh, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            try
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    hocsinh.name = await taifile(fThumb);
                }
                if (string.IsNullOrEmpty(hocsinh.name)) hocsinh.name = "default.jpg";
                var hocsinhID = HttpContext.Session.GetString("HocSinhId");
                var taikhoanID = HttpContext.Session.GetString("AccountId");
                hocsinh.IdGiaoVien = int.Parse(taikhoanID);
                hocsinh.IdHocSinh = int.Parse(hocsinhID);
                _context.Files.Add(hocsinh);
                _context.SaveChanges();
                _notyfService.Success("Update Success");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/AdminHocSinhs/Delete/5
    }
}
