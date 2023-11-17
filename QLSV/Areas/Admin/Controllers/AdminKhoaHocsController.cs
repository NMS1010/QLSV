using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSV.InterfacesService;
using QLSV.Models;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminKhoaHocsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GameStoreDbContext _context;
        private IUploadService _uploadService;
        public INotyfService _notyfService { get; }

        public AdminKhoaHocsController(IUnitOfWork unitOfWork, INotyfService notyfService, GameStoreDbContext context, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _context = context;
            _uploadService = uploadService;
        }

        // Lich khoa hoc
        public async Task<IActionResult> IndexLichKhoaHoc(int id)
        {
            var collection = await _context.LichKhoaHocs
                .Include(x => x.KhoaHoc)
                .Where(x => x.IdKhoaHoc == id)
                .ToListAsync();

            ViewBag.khoahoc = _context.KhoaHocs.Find(id);
            ViewBag.IdKhoaHoc = id;
            return View(collection);
        }

        public IActionResult CreateLichKhoaHoc(int id)
        {
            ViewBag.IdKhoaHoc = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLichKhoaHoc([FromForm] LichKhoaHoc lichKhoaHoc)
        {
            await _context.LichKhoaHocs.AddAsync(lichKhoaHoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexLichKhoaHoc), new { id = lichKhoaHoc.IdKhoaHoc });
        }

        public async Task<IActionResult> EditLichKhoaHoc(int id)
        {
            var lichKhoaHoc = await _context.LichKhoaHocs.FindAsync(id);
            return View(lichKhoaHoc);
        }

        [HttpPost]
        public async Task<IActionResult> EditLichKhoaHoc([FromForm] LichKhoaHoc lichKhoaHoc)
        {
            var lkh = await _context.LichKhoaHocs.FindAsync(lichKhoaHoc.Id);
            lkh.Thu = lichKhoaHoc.Thu;
            lkh.ThoiGian = lichKhoaHoc.ThoiGian;
            _context.LichKhoaHocs.Update(lkh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexLichKhoaHoc), new { id = lichKhoaHoc.IdKhoaHoc });
        }

        public async Task<IActionResult> DeleteLichKhoaHoc(int id)
        {
            var lich = await _context.LichKhoaHocs.Include(x => x.KhoaHoc).Where(x => x.Id == id).FirstOrDefaultAsync();
            await _context.LichKhoaHocs.DeleteByKeyAsync(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexLichKhoaHoc), new { id = lich.IdKhoaHoc });
        }

        // file khoa hoc
        public async Task<IActionResult> IndexFileKhoaHoc(int id)
        {
            var collection = await _context.FileKhoaHocs
                .Where(x => x.IdKhoaHoc == id)
                .ToListAsync();
            ViewBag.khoahoc = _context.KhoaHocs.Find(id);
            ViewBag.IdKhoaHoc = id;
            return View(collection);
        }

        public IActionResult CreateFileKhoaHoc(int id)
        {
            ViewData["IdKhoaHoc"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFileKhoaHoc([FromForm] string name, [FromForm] int IdKhoaHoc, [FromForm] IFormFile file)
        {
            var lichKhoaHoc = new FileKhoaHoc()
            {
                Name = name,
                Path = await _uploadService.SaveFile(file),
                IdKhoaHoc = IdKhoaHoc
            };
            await _context.FileKhoaHocs.AddAsync(lichKhoaHoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexFileKhoaHoc), new { id = lichKhoaHoc.IdKhoaHoc });
        }

        public async Task<IActionResult> EditFileKhoaHoc(int id)
        {
            var lichKhoaHoc = await _context.FileKhoaHocs.FindAsync(id);
            return View(lichKhoaHoc);
        }

        [HttpPost]
        public async Task<IActionResult> EditFileKhoaHoc([FromForm] FileKhoaHoc fKhoaHoc, [FromForm] IFormFile file)
        {
            var f = await _context.FileKhoaHocs.FindAsync(fKhoaHoc.Id);
            f.Name = fKhoaHoc.Name;
            var p = "";
            if (file != null)
            {
                p = f.Path;
                f.Path = await _uploadService.SaveFile(file);
            }
            _context.FileKhoaHocs.Update(f);
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(p)) await _uploadService.DeleteFile(p);
            return RedirectToAction(nameof(IndexFileKhoaHoc), new { id = f.IdKhoaHoc });
        }

        public async Task<IActionResult> DeleteFileKhoaHoc(int id)
        {
            var obj = await _context.FileKhoaHocs
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            await _context.FileKhoaHocs.DeleteByKeyAsync(id);
            await _context.SaveChangesAsync();
            await _uploadService.DeleteFile(obj.Path);
            return RedirectToAction(nameof(IndexFileKhoaHoc), new { id = obj.IdKhoaHoc });
        }

        // thong bao khoa hoc
        public async Task<IActionResult> IndexThongBaoKhoaHoc(int id)
        {
            var collection = await _context.ThongBaoKhoaHocs
                .Where(x => x.IdKhoaHoc == id)
                .ToListAsync();
            ViewBag.khoahoc = _context.KhoaHocs.Find(id);
            ViewBag.IdKhoaHoc = id;
            return View(collection);
        }

        public IActionResult CreateThongBaoKhoaHoc(int id)
        {
            ViewData["IdKhoaHoc"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateThongBaoKhoaHoc([FromForm] ThongBaoKhoaHoc tb)
        {
            await _context.ThongBaoKhoaHocs.AddAsync(tb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexThongBaoKhoaHoc), new { id = tb.IdKhoaHoc });
        }

        public async Task<IActionResult> EditThongBaoKhoaHoc(int id)
        {
            var tb = await _context.ThongBaoKhoaHocs.FindAsync(id);
            return View(tb);
        }

        [HttpPost]
        public async Task<IActionResult> EditThongBaoKhoaHoc([FromForm] ThongBaoKhoaHoc tb)
        {
            var noti = await _context.ThongBaoKhoaHocs.FindAsync(tb.Id);
            noti.TieuDe = tb.TieuDe;
            noti.NoiDung = tb.NoiDung;
            _context.ThongBaoKhoaHocs.Update(noti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexThongBaoKhoaHoc), new { id = noti.IdKhoaHoc });
        }

        public async Task<IActionResult> DeleteThongBaoKhoaHoc(int id)
        {
            var obj = await _context.ThongBaoKhoaHocs.Where(x => x.Id == id).FirstOrDefaultAsync();

            await _context.ThongBaoKhoaHocs.DeleteByKeyAsync(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexThongBaoKhoaHoc), new { id = obj.IdKhoaHoc });
        }

        // GET: Admin/AdminKhoaHocs
        public IActionResult Index()
        {
            var collection = _context.KhoaHocs.Include(x => x.GiaoVien).ToList();
            return View(collection);
        }

        // GET: Admin/AdminKhoaHocs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KhoaHoc = _context.KhoaHocs.Include(x => x.GiaoVien).Where(x => x.Id == id).FirstOrDefault();
            if (KhoaHoc == null)
            {
                return NotFound();
            }

            return View(KhoaHoc);
        }

        // GET: Admin/AdminKhoaHocs/Create
        public IActionResult Create()
        {
            ViewBag.GiaoVien = _context.GiaoViens.ToList();
            return View();
        }

        // POST: Admin/AdminKhoaHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,course_name,description,gia,NgayBatDau,NgayKetThuc, IdGiaoVien")] KhoaHoc khoahoc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                //Xu ly Image
                try
                {
                    if (fThumb != null)
                    {
                        khoahoc.Image = await _uploadService.SaveFile(fThumb);
                    }
                    if (string.IsNullOrEmpty(khoahoc.Image)) khoahoc.Image = "default.jpg";
                    _context.KhoaHocs.Add(khoahoc);
                    _context.SaveChanges();
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

        // GET: Admin/AdminKhoaHocs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.GiaoVien = _context.GiaoViens.ToList();
            var KhoaHoc = _context.KhoaHocs.Include(x => x.GiaoVien).Where(x => x.Id == id).FirstOrDefault();
            if (KhoaHoc == null)
            {
                return NotFound();
            }
            return View(KhoaHoc);
        }

        // POST: Admin/AdminKhoaHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,course_name,description,gia,NgayBatDau,NgayKetThuc, IdGiaoVien")] KhoaHoc khoahoc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kh = await _context.KhoaHocs.FindAsync(khoahoc.Id);
                    kh.course_name = khoahoc.course_name;
                    kh.description = khoahoc.description;
                    kh.gia = khoahoc.gia;
                    kh.NgayBatDau = khoahoc.NgayBatDau;
                    kh.NgayKetThuc = khoahoc.NgayKetThuc;
                    kh.IdGiaoVien = khoahoc.IdGiaoVien;

                    if (fThumb != null)
                    {
                        kh.Image = await _uploadService.SaveFile(fThumb);
                    }
                    if (string.IsNullOrEmpty(kh.Image)) kh.Image = "user-content/default.jpg";
                    _context.KhoaHocs.Update(kh);
                    _context.SaveChanges();
                    _notyfService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/AdminKhoaHocs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KhoaHoc = _context.KhoaHocs.Find(id);
            if (KhoaHoc == null)
            {
                return NotFound();
            }

            return View(KhoaHoc);
        }

        // POST: Admin/AdminKhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var KhoaHoc = _context.KhoaHocs.Find(id);
                _context.KhoaHocs.Remove(KhoaHoc);
                _context.SaveChanges();
                _notyfService.Success("Xóa thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}