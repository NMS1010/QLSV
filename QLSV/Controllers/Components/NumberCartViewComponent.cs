using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Extension;
using QLSV.ModelViews;
using QLSV.Models;

namespace QLSV.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<Cart>>("_GioHang");
            return View(cart);
        }
    }
}
