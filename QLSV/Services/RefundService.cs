using QLSV.Enums;
using QLSV.Interfaces;
using QLSV.InterfacesRepository;
using QLSV.InterfacesService;
using QLSV.Models;
using QLSV.OtpModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace QLSV.Services
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository _refundRepository;
        private readonly IHocSinhService _HocSinhService;
        private readonly IHocSinhRepository _HocSinhRepository;
        public DbContext Context { get; }
        public RefundService(IRefundRepository refundRepository, 
            IHocSinhService HocSinhService, 
            IHocSinhRepository HocSinhRepository,
            GameStoreDbContext context)
        {
            _refundRepository = refundRepository;
            _HocSinhService = HocSinhService;
            _HocSinhRepository = HocSinhRepository;
            Context = context;
        }

        public Refund refund(int userID, int productID)
        {
            RefundRequest request=_refundRepository.refundRequest(productID,userID);
            Refund rf=new Refund()
            {
                UserID=userID,
                OrderID=request.OrderID,
                ProductID=productID,
                Price=request.Price,
                Status=(int)RefundType.pending,
                DatePurchase=request.DatePurchase,
                DateCreate=DateTime.Now
            };
            return rf;
        }
        public void refundtoallUser()
        {
            var userRefunds=_refundRepository.GetRefundHocSinh();
            foreach(var user in userRefunds)
            {
                var userRefund = _HocSinhService.updateBalance(user.UserID, user.Price, (int)marketType.sell);
                var rf = _refundRepository.GetById(user.Id);
                rf.Status = (int)RefundType.accept;
                _HocSinhRepository.Update(userRefund);
                _refundRepository.Update(rf);
            }
            Context.SaveChanges();
        }
    }
}
