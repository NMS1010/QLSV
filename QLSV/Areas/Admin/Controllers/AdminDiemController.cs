using AspNetCoreHero.ToastNotification.Abstractions;
using QLSV.Models;
using QLSV.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDiemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        private readonly GameStoreDbContext _context;

        public AdminDiemController(IUnitOfWork unitOfWork, INotyfService notyfService, GameStoreDbContext context)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _context = context;
        }

        // GET: thong ke
        public ActionResult ThongKe(int idKhoaHoc)
        {
            var khoahoc = _context.KhoaHocs.Where(x => x.Id == idKhoaHoc)
                .Include(x => x.DiemHocSinhs)
                .ThenInclude(x => x.HocSinh).FirstOrDefault();

            ViewData["1-2"] = khoahoc.DiemHocSinhs.Count(x => x.SoDiem >= 1 && x.SoDiem < 2);
            ViewData["2-4"] = khoahoc.DiemHocSinhs.Count(x => x.SoDiem >= 2 && x.SoDiem < 4);
            ViewData["4-6"] = khoahoc.DiemHocSinhs.Count(x => x.SoDiem >= 4 && x.SoDiem < 6);
            ViewData["6-8"] = khoahoc.DiemHocSinhs.Count(x => x.SoDiem >= 6 && x.SoDiem < 8);
            ViewData["8-10"] = khoahoc.DiemHocSinhs.Count(x => x.SoDiem >= 8 && x.SoDiem <= 10);

            var topHsIncrease = khoahoc.DiemHocSinhs.OrderByDescending(x => x.SoDiem).Take(5).ToList();
            ViewData["top-5-increase"] = topHsIncrease;

            var topHsDescrease = khoahoc.DiemHocSinhs.OrderBy(x => x.SoDiem).Take(5).ToList();
            ViewData["top-5-descrease"] = topHsDescrease;

            ViewBag.khoahoc = khoahoc;

            return View();
        }

        // GET: SaleController
        public ActionResult Index()
        {
            var sale = _unitOfWork.KhoaHocRepository.GetAll();
            var role = HttpContext.Session.GetString("Role");
            if (role == "Dev")
            {
                //var taikhoanID = HttpContext.Session.GetString("AccountId");
                //var khachhang = _unitOfWork.GiaoVienRepository.GetById(int.Parse(taikhoanID));
                //return RedirectToAction("Edit", new { id = khachhang.IdKhoaHoc });
                var taikhoanID = HttpContext.Session.GetString("AccountId");
                var khachhang = _context.GiaoViens.Include(x => x.KhoaHocs).Where(x => x.Id == int.Parse(taikhoanID)).FirstOrDefault();
                sale = khachhang.KhoaHocs;
            }
            return View(sale);
        }

        // GET: SaleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult ViewProductSale(int id)
        {
            try
            {
                HttpContext.Session.SetInt32("SaleId", id);
                var ls = _context.DiemHocSinhs.Include(x => x.HocSinh).Where(x => x.IdKhoaHoc == id).Select(x => x.HocSinh).ToList();
                ViewBag.khoahoc = _context.KhoaHocs.Find(id);
                return View(ls);
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult ViewProductSaleInDate()
        {
            try
            {
                var ls = _unitOfWork.HocSinhRepository.listhocsinhdamua().ToList();
                return View(ls);
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: AdminProductsController/Edit/5
        public ActionResult Edits(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleid = HttpContext.Session.GetInt32("SaleId");
            var product = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdHocSinh == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edits(int id, DiemHocSinh saleModelView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var solan = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdHocSinh == saleModelView.IdHocSinh && t.IdKhoaHoc == saleModelView.IdKhoaHoc);
                    DiemHocSinh sale = new DiemHocSinh()
                    {
                        IdHocSinh = id,
                        IdKhoaHoc = saleModelView.IdKhoaHoc,
                        SoDiem = saleModelView.SoDiem,
                        NhanXet = saleModelView.NhanXet,
                        SoLan = solan.Count() + 1
                    };
                    _unitOfWork.DiemHocSinhRepository.Update(sale);
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
            return View(saleModelView);
        }

        public ActionResult DeleteSeleProduct(int id)
        {
            try
            {
                var product = _unitOfWork.DiemHocSinhRepository.GetById(id);
                _unitOfWork.DiemHocSinhRepository.Delete(product);
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

        // GET: SaleController/Edit/5
        public ActionResult Edit(int id)
        {
            var ls = _context.DiemHocSinhs.Where(x => x.IdKhoaHoc == id)
                    .Include(x => x.HocSinh)
                    .Include(x => x.KhoaHoc)
                    .ToList();
            ViewBag.SaleId = id.ToString();
            ViewBag.khoahoc = _context.KhoaHocs.Find(id);
            return View(ls);
        }

        [HttpPost]
        [Route("/Diem/AjaxMethod", Name = "AjaxMethod")]
        public JsonResult AjaxMethod(string diemId, string productId, string discount, string nhanxet)
        {
            if (_unitOfWork.KhoaHocRepository.GetById(int.Parse(productId)) == null)
                return null;
            try
            {
                var diem = _context.DiemHocSinhs.Find(int.Parse(diemId));
                diem.SoDiem = double.Parse(discount);
                diem.NhanXet = nhanxet;
                _unitOfWork.DiemHocSinhRepository.Update(diem);
                _unitOfWork.SaveChange();
            }
            catch (Exception)
            {
                throw;
            }

            return Json(1);
        }

        [HttpPost]
        public void SaveSaleProduct(string saleId, string productId, string discount)
        {
        }

        // POST: SaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void deletesale(int id)
        {
            var sale = _unitOfWork.KhoaHocRepository.GetById(id);
            _unitOfWork.KhoaHocRepository.Delete(sale);
            _unitOfWork.SaveChange();
        }

        // POST: SaleController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdKhoaHoc == id).ToList();
                foreach (var item in product)
                {
                    _unitOfWork.DiemHocSinhRepository.Delete(item);
                    _unitOfWork.SaveChange();
                }

                deletesale(id);
                _notyfService.Success("Delete successful");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}