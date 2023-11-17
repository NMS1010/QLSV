using QLSV.Interfaces;
using QLSV.InterfacesRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace QLSV
{
    public interface IUnitOfWork:IDisposable
    {
        DbContext Context { get; }
        void SaveChange();
        IProductRepository ProductRepository { get; }
        IGiaoVienRepository GiaoVienRepository { get; }   
        IHocSinhRepository HocSinhRepository { get; }
        IDiemHocSinhRepository DiemHocSinhRepository { get; }
        IKhoaHocRepository KhoaHocRepository { get; }
        IOrderRepository OrderRepository { get; }
        IRefundRepository RefundRepository { get; }
        IAdminRepository AdminRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IFundRepository FundRepository { get; }
        IBlogRepository BlogRepository { get; }
        IAddFundTransactionRepository AddFundTransactionRepository { get; }
    }
}
