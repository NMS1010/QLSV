using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using QLSV;
using AspNetCoreHero.ToastNotification.Abstractions;
using QLSV.OtpModels;
using QLSV.ModelViews;
using Microsoft.EntityFrameworkCore;

namespace DuAnGame.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Dev")]
    [Area("Admin")]
    [Route("admin.html", Name = "AdminIndex")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GameStoreDbContext _context;
        public INotyfService _notyfService { get; }

        public HomeController(IUnitOfWork unitOfWork, INotyfService notyfService, GameStoreDbContext context)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _context = context;
        }

        public IActionResult Index()
        {
            var topproduct = _unitOfWork.KhoaHocRepository.GetAll();
            var orders = _context.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .Where(t => t.DatePurchase.Month == DateTime.Now.Month).ToList();
            var sale = _unitOfWork.KhoaHocRepository.GetAll();
            if (topproduct != null)
            {
                ViewBag.Topproduct = topproduct;
            }
            ViewBag.Sale = sale;
            ViewBag.orders = orders;
            return View();
        }
    }
}