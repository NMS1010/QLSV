using AspNetCoreHero.ToastNotification.Abstractions;
using QLSV.Enums;
using QLSV.Models;
using QLSV.ModelViews;
using QLSV.OtpModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Controllers
{
    public class SearchProController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public INotyfService _notyfService { get; }
        public SearchProController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        int release = (int)productType.release;
        public string NamePro
        {
            get
            {
                var gh = HttpContext.Session.GetString("NamePro");
                if (gh == null)
                {
                    gh = "";
                }
                return gh;
            }
        }
        public string NameItem
        {
            get
            {
                var gh = HttpContext.Session.GetString("NameItem");
                if (gh == null)
                {
                    gh = "";
                }
                return gh;
            }
        }


        [HttpGet]
        public IActionResult FindProductsByName()
        {
            return View();
        }

        //[Route("/SearchPro/ProductsByName/name={name}.html", Name = ("ProductSearch"))]
        //public IActionResult ProductsByName(string name, int? page)
        //{
        //    ViewBag.nameSearch = name.Trim();
        //    HttpContext.Session.SetString("NamePro", name);
        //    var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        //    var pageSize = 6;
        //    List<SaleModelView> products = new List<SaleModelView>();
        //    if (name == "all")
        //    {
        //        products = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t=>t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
        //    }
        //    else
        //    {
        //        products = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t=>t.ProductName.Contains(name,StringComparison.OrdinalIgnoreCase) && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
        //    }
        //    if (products.Count() <= 6)
        //        ViewBag.maxPage = 1;
        //    else
        //    {
        //        double dMaxPage = Convert.ToDouble(products.Count());
        //        ViewBag.maxPage = Math.Ceiling(dMaxPage / 6);
        //    }
        //    var pl = products.AsQueryable().ToPagedList(pageNumber, pageSize);
        //    var plr = pl.ToList();
        //    ViewBag.CurrentPage = pageNumber;
        //    ViewData["GiaoViens"] = new SelectList(_unitOfWork.GiaoVienRepository.GetAll(), "Name", "Name");
        //    ViewData["Categories"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Name", "Name");
        //    return View(plr);
        //}

        //[HttpPost]
        //public IActionResult SearchDanhMuc(string devId, string catId, int? page)
        //{
        //    var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        //    var pageSize = 6;
        //    List<Productdetail> products = new List<Productdetail>();
        //    if (devId != null)
        //    {
        //        products = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.DevName == devId).ToList();
        //    }
        //    else if (catId != null)
        //    {
        //        products = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.CatID == catId).ToList();
        //    }
        //    if (products.Count() <= 6)
        //        ViewBag.maxPage = 1;
        //    else
        //    {
        //        double dMaxPage = Convert.ToDouble(products.Count());
        //        ViewBag.maxPage = Math.Ceiling(dMaxPage / 6);
        //    }
        //    var pl = products.AsQueryable().ToPagedList(pageNumber, pageSize);
        //    var plr = pl.ToList();
        //    ViewBag.CurrentPage = pageNumber;
        //    return View(plr);
        //}
    }
}
