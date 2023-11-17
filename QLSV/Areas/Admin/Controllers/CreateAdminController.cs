//using AspNetCoreHero.ToastNotification.Abstractions;
//using QLSV.Areas.Admin.Models;
//using QLSV.Helpper;
//using QLSV.Models;
//using QLSV.ModelViews;
//using QLSV.OtpModels;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace QLSV.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Authorize(Roles = "Admin")]
//    public class CreateAdminController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public INotyfService _notyfService { get; }
//        public CreateAdminController(IUnitOfWork unitOfWork, INotyfService notyfService)
//        {
//            _unitOfWork = unitOfWork;
//            _notyfService = notyfService;
//        }
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var product = _unitOfWork.GiaoVienRepository.GetById((int)id);
//            if (product == null)
//            {
//                return NotFound();
//            }
//            return View(product);
//        }

//        // POST: AdminProductsController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, [Bind("Id,Name,Passwork,UserName")] GiaoVien GiaoVien)
//        {
//            if (id != GiaoVien.Id)
//            {
//                return NotFound();
//            }
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _unitOfWork.GiaoVienRepository.Update(GiaoVien);
//                    _unitOfWork.SaveChange();
//                    _notyfService.Success("Update successful");
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (Exception)
//                {

//                    _notyfService.Error("Error");
//                    return RedirectToAction(nameof(Index));
//                }

//            }
//            return View(GiaoVien);
//        }
//        public ActionResult Delete(int id)
//        {
//            try
//            {
//                var product = _unitOfWork.GiaoVienRepository.GetById(id);
//                _unitOfWork.GiaoVienRepository.Delete(product);
//                _unitOfWork.SaveChange();
//                _notyfService.Success("Delete successful");
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception)
//            {
//                _notyfService.Error("Error");
//                return RedirectToAction(nameof(Index));
//            }
//        }
//        public ActionResult Index()
//        {
//            var ls = _unitOfWork.GiaoVienRepository.GetAll().ToList();
//            return View(ls);
//        }
//        public IActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//            var dev = _unitOfWork.GiaoVienRepository.GetAll().Where(t => t.Id == id).FirstOrDefault();
//            HttpContext.Session.SetString("ProductID", id.ToString());
//            return View(dev);
//        }
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: AdminProductsController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create([Bind("Id,full_name,email,password,phone_number,address")] GiaoVien GiaoVien)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    if (GiaoVien.email != null)
//                    {
//                        _unitOfWork.GiaoVienRepository.Create(GiaoVien);
//                        _unitOfWork.SaveChange();
//                        Utilities.sendemaildev(GiaoVien.email, GiaoVien);
//                        _notyfService.Success("Successfully added new");
//                        return RedirectToAction(nameof(Index));
//                    }
//                    else
//                    {
//                        _notyfService.Error("Error");
//                        return RedirectToAction(nameof(Index));
//                    }
//                }
//                catch (Exception)
//                {
//                    _notyfService.Error("Error");
//                    return RedirectToAction(nameof(Index));
//                }

//            }
//            return View(GiaoVien);
//        }
//        public string Role
//        {
//            get
//            {
//                var gh = HttpContext.Session.GetString("Role");
//                if (gh == null)
//                {
//                    gh = "";
//                }
//                return gh;
//            }
//        }
//        [Route("tai-khoan.html", Name = "Info")]
//        public IActionResult Info()
//        {
//            var taikhoanID = HttpContext.Session.GetString("AccountId");
//            if (taikhoanID != null)
//            {
//                var khachhang = _unitOfWork.AdminRepository.GetAll().SingleOrDefault(x => x.TaiKhoan == taikhoanID);
//                if (khachhang != null)
//                {
//                    return View(khachhang);
//                }
//            }
//            return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
//        }
//        [AllowAnonymous]
//        [Route("LoginHocSinh.html", Name = "LoginHocSinh")]
//        public IActionResult AdminLogin(string returnUrl = null)
//        {
//            var taikhoanID = HttpContext.Session.GetString("AccountId");
//            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }
//        [HttpPost]
//        [AllowAnonymous]
//        [Route("LoginHocSinh.html", Name = "LoginHocSinh")]
//        public async Task<IActionResult> AdminLogin(LoginViewModel model, string returnUrl = null)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    if (User.IsInRole("User"))
//                    {
//                        _notyfService.Warning("Please log out at User");
//                        return RedirectToAction("Dashboard", "HocSinh");
//                    }
//                    var kh = _unitOfWork.AdminRepository.GetAll().SingleOrDefault(x => x.TaiKhoan.Trim() == model.Gmail);

//                    if (kh == null)
//                    {
//                        ViewBag.Eror = "Login information is incorrect";
//                        return View(model);
//                    }
//                    string pass = (model.Password.Trim());
//                    // + kh.Salt.Trim()
//                    if (kh.Password.Trim() != pass)
//                    {
//                        ViewBag.Eror = "Login information is incorrect";
//                        return View(model);
//                    }
//                    //đăng nhập thành công

//                    //ghi nhận thời gian đăng nhập
//                    kh.LastLogin = DateTime.Now;
//                    _unitOfWork.AdminRepository.Update(kh);
//                    _unitOfWork.SaveChange();


//                    var taikhoanID = HttpContext.Session.GetString("AccountId");
//                    //identity
//                    //luuw seccion Makh
//                    HttpContext.Session.SetString("AccountId", kh.TaiKhoan.ToString());
//                    HttpContext.Session.SetString("Role", "Admin");
//                    var Roles = HttpContext.Session.GetString("Role");
//                    //identity
//                    var userClaims = new List<Claim>
//                        {
//                            new Claim(ClaimTypes.Name, kh.HoTen),
//                            new Claim(ClaimTypes.Email, kh.TaiKhoan),
//                            new Claim("AccountId", kh.TaiKhoan.ToString()),
//                            new Claim(ClaimTypes.Role, Roles)
//                        };
//                    var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
//                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
//                    await HttpContext.SignInAsync(userPrincipal);



//                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
//                }
//            }
//            catch
//            {
//                return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
//            }
//            return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
//        }
//        [HttpPost]
//        public IActionResult ChangePassword(ChangePasswordViewModel model)
//        {
//            try
//            {
//                var taikhoanID = HttpContext.Session.GetString("AccountId");
//                if (taikhoanID == null)
//                {
//                    return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
//                }
//                if (ModelState.IsValid)
//                {
//                    var taikhoan = _unitOfWork.AdminRepository.GetAll().Where(t => t.TaiKhoan == taikhoanID).FirstOrDefault();
//                    if (taikhoan == null) return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
//                    var pass = (model.PasswordNow.Trim());
//                    {
//                        string passnew = (model.Password.Trim());
//                        taikhoan.Password = passnew;
//                        _unitOfWork.AdminRepository.Update(taikhoan);
//                        _unitOfWork.SaveChange();
//                        _notyfService.Success("Change password successfully");
//                        return RedirectToAction("Info", "Admin", new { Area = "Admin" });
//                    }
//                }
//            }
//            catch
//            {
//                _notyfService.Warning("Password change failed");
//                return RedirectToAction("Info", "Admin", new { Area = "Admin" });
//            }
//            _notyfService.Warning("Password change failed");
//            return RedirectToAction("Info", "Admin", new { Area = "Admin" });
//        }
//        [Route("logout.html", Name = "Logout")]
//        public IActionResult AdminLogout()
//        {
//            try
//            {
//                HttpContext.SignOutAsync();
//                HttpContext.Session.Remove("AccountId");
//                HttpContext.Session.Remove("Role");
//                return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
//            }
//            catch
//            {
//                return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
//            }
//        }
//    }
//}
