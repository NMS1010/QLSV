using AspNetCoreHero.ToastNotification.Abstractions;
using QLSV;
using QLSV.Enums;
using QLSV.Extension;
using QLSV.Models;
using QLSV.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DuAnGame.Controllers
{
    public class ProductCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        private readonly GameStoreDbContext _context;

        public ProductCartController(IUnitOfWork unitOfWork, INotyfService notyfService, GameStoreDbContext context)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _context = context;
        }

        public List<Cart> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<Cart>>("_GioHang");
                if (gh == default(List<Cart>))
                {
                    gh = new List<Cart>();
                }
                return gh;
            }
        }

        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID)
        {
            List<Cart> cart = GioHang;
            try
            {
                var userId = HttpContext.Session.GetString("CustomerId");

                var t = _context.DiemHocSinhs.Any(x => x.IdHocSinh == int.Parse(userId) && productID == x.IdKhoaHoc);

                if (t)
                {
                    throw new Exception();
                }
                //Them san pham vao gio hang
                Cart item = cart.SingleOrDefault(p => p.product.Id == productID);
                if (item != null)
                {
                    HttpContext.Session.Set<List<Cart>>("_GioHang", cart);
                }
                else
                {
                    KhoaHoc hh = _unitOfWork.ProductRepository.GetById(productID);
                    item = new Cart
                    {
                        product = hh
                    };
                    cart.Add(item);//Them vao gio
                }
                //Luu lai Session
                HttpContext.Session.Set<List<Cart>>("_GioHang", cart);
                ViewBag.GioHang = cart;
                _notyfService.Success("More successful products");
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public void sendemail(string emailaddress, List<Cart> cart, int id_dh)
        {
            if (emailaddress.Length == 0)
            {
            }
            else
            {
                if (QLSV.Helpper.Utilities.IsValidEmail(emailaddress))
                {
                    SmtpClient client = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential()
                        {
                            UserName = "sondovipro123@gmail.com",
                            Password = "caofqthenhkakkgl"
                        }
                    };
                    MailAddress fromemail = new MailAddress("sondovipro123@gmail.com", "admin");
                    MailAddress toemail = new MailAddress(emailaddress, "someone");
                    MailMessage mess = new MailMessage()
                    {
                        From = fromemail,
                        Subject = "Congratulations on your successful order",
                        IsBodyHtml = true,
                    };
                    mess.Body += "<h1>Hello:" + User.Identity.Name + "</h1>";
                    mess.Body += "<h3>Your order has been placed successfully<h3>";
                    mess.Body += "<h3>Your order number:" + id_dh.ToString() + "</h3>";
                    mess.Body += "<h3>List of ordered items</h3>";
                    mess.Body += "<table><thead>";
                    mess.Body += "<tr><th>Product code</th><th>Product name</th><th>Quantity</th><th>Price</th></thead>";
                    mess.Body += "<tbody>";
                    decimal countprice = 0;
                    foreach (var item in cart)
                    {
                        mess.Body += "<tr><td>" + item.product.Id.ToString() + "</td>" + "<td>" + item.product.course_name + "</td>" + "<td>" + 1 + "</td>" + "<td>" + item.product.gia.ToString() + "Đ</td></tr>";
                        countprice += item.product.gia;
                    }
                    mess.Body += "</tbody></table>";
                    mess.Body += "<h4>Total price:" + countprice.ToString() + "$</h4>";
                    mess.To.Add(toemail);
                    client.Send(mess);
                }
                else
                {
                }
            }
        }

        [Authorize, HttpPost]
        public IActionResult ThanhToan()
        {
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.Get<List<Cart>>("_GioHang");
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                var maKH = _unitOfWork.HocSinhRepository.GetById(int.Parse(taikhoanID));
                var totalprice = cart.Sum(t => t.product.gia);
                int type = (int)marketType.buy;
                if (maKH.Balance < totalprice)
                {
                    _notyfService.Warning("The amount is not enough");
                    return RedirectToRoute("Cart");
                }
                try
                {
                    var item = _unitOfWork.OrderRepository.createOrder(int.Parse(taikhoanID.ToString()), cart);
                    _unitOfWork.OrderRepository.Create(item);
                    _unitOfWork.DiemHocSinhRepository.updateDiemHocSinh(int.Parse(taikhoanID.ToString()), cart);
                    _unitOfWork.HocSinhRepository.updateBalance(int.Parse(taikhoanID.ToString()), totalprice, type);
                    _unitOfWork.SaveChange();
                    int madh = _unitOfWork.OrderRepository.orderID(int.Parse(taikhoanID));
                    sendemail(maKH.Gmail, cart, madh);
                    HttpContext.Session.Remove("_GioHang");
                    _notyfService.Success("Success");
                    return RedirectToRoute("Cart");
                }
                catch (Exception ex)
                {
                    //log
                    return Redirect("/ProductCart/CheckoutFail");
                }
            }
            return RedirectToRoute("Cart");
        }

        public IActionResult CheckoutFail()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Chưa thanh toán"
            //Xóa session
            return View();
        }

        public IActionResult CheckoutSuccess()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Paypal" và thành công
            //Xóa session
            return View();
        }

        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int productID)
        {
            try
            {
                List<Cart> gioHang = GioHang;
                Cart item = gioHang.SingleOrDefault(p => p.product.Id == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                //luu lai session
                HttpContext.Session.Set<List<Cart>>("_GioHang", gioHang);
                var cart = HttpContext.Session.Get<List<Cart>>("_GioHang");
                ViewBag.GioHang = cart;
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public void GetDiemHocSinh()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            ViewBag.GetId = taikhoanID;
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.HocSinhRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var proLib = _unitOfWork.DiemHocSinhRepository.getDiemHocSinh(khachhang.Id);
                    ViewBag.DonHang = proLib;
                }
            }
        }

        [Route("cart.html", Name = "Cart")]
        public ActionResult Cart()
        {
            //GetDiemHocSinh();
            return View(GioHang);
        }
    }
}