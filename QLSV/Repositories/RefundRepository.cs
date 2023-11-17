using Dapper;
using QLSV.Interfaces;
using QLSV.Models;
using QLSV.OtpModels;
using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace QLSV.Repositories
{
    public class RefundRepository:GameStoreRepository<Models.Refund>,IRefundRepository
    {
        private readonly IHocSinhRepository _HocSinhRepository;
        public RefundRepository(GameStoreDbContext context,IHocSinhRepository HocSinhRepository):base(context)
        {
            _HocSinhRepository = HocSinhRepository;
        }
        public OtpModels.RefundRequest refundRequest(int productID, int UserID)
        {
            var query = @"select top 1.[o].id as OrderID,UserID,ProductID,Price,DatePurchase from [Order] o,OrderDetail od
                        where o.Id=od.Id and ProductID=@productid AND UserID=@userid
                        order by DatePurchase desc";
            var parameter = new DynamicParameters();
            parameter.Add("productid",productID);
            parameter.Add("userid", UserID);
            var request = Context.Database.GetDbConnection().QuerySingle<OtpModels.RefundRequest>(query, parameter);
            return request;
        }
        public List<KhoaHoc> listgameRefund(int userid)
        {
            var query = @"SELECT KhoaHoc.*
FROM HocSinh
INNER JOIN [Order] ON HocSinh.Id = [Order].UserID
INNER JOIN OrderDetail ON [Order].Id = OrderDetail.Id
INNER JOIN KhoaHoc ON OrderDetail.ProductID = KhoaHoc.Id
WHERE [Order].DatePurchase >= DATEADD(day, -10, GETDATE()) and HocSinh.Id = @userid";
            var parameter = new DynamicParameters();
            parameter.Add("userid", userid);
            var result = Context.Database.GetDbConnection().Query<KhoaHoc>(query, parameter);
            return result.ToList();
        }
        public List<RefundUser> GetRefundHocSinh()
        {
            var query = @"select Id,UserID,Price from Refund where Status=0";
            var result=Context.Database.GetDbConnection().Query<RefundUser>(query);
            return result.ToList();
        }
    }
}
