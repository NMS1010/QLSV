using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.ModelViews;
using QLSV.Services;
using System;
using System.Threading.Tasks;

namespace QLSV.Controllers
{
    public class RechargeController : Controller
    {
        private readonly GameStoreDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotyfService _notyfService;

        public RechargeController(GameStoreDbContext context, IHttpContextAccessor httpContextAccessor, INotyfService notyfService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _notyfService = notyfService;
        }

        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var req = _httpContextAccessor?.HttpContext?.Request;
            var path = $"{req?.Scheme}://{req?.Host}";

            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayService();
            var urlCallBack = path + "/Recharge/PaymentCallback";

            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", "0YNGKFBX");
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", "ZIMIGZSAVBDTUENJAKAGBOWWPXZGLBCC");

            return paymentUrl;
        }

        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayService();
            var response = pay.GetFullResponseData(collections, "ZIMIGZSAVBDTUENJAKAGBOWWPXZGLBCC");

            return response;
        }

        [HttpPost]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = CreatePaymentUrl(model, HttpContext);
            HttpContext.Session.SetString("Amount", model.Amount.ToString());
            return Redirect(url);
        }

        public async Task<IActionResult> PaymentCallback()
        {
            var response = PaymentExecute(Request.Query);
            if (response.TransactionId != "0")
            {
                var amount = HttpContext.Session.GetString("Amount");
                var userId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                var user = _context.HocSinhs.Find(userId);

                user.Balance += int.Parse(amount);

                _context.HocSinhs.Update(user);

                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Amount", "");
                _notyfService.Success("Nạp tiền thành công");
            }
            else
            {
                _notyfService.Error("Nạp tiền thất bại");
            }
            return Redirect("/tai-khoan-cua-toi.html");
        }

        public IActionResult Index()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            return View();
        }
    }
}