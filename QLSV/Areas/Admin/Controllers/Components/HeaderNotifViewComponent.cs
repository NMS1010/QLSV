using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Extension;
using QLSV.ModelViews;
using QLSV.Models;
using System.Linq;

namespace QLSV.Areas.Admin.Controllers
{
    public class HeaderNotifViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public HeaderNotifViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            var pro = _unitOfWork.KhoaHocRepository.GetAll().Take(3).ToList();
            return View(pro);
        }
    }
}
