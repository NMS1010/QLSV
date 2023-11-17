using QLSV.Interfaces;
using QLSV.InterfacesRepository;
using Microsoft.EntityFrameworkCore;

namespace QLSV
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        public IProductRepository ProductRepository { get; set; }   
        public IGiaoVienRepository GiaoVienRepository { get; set; }
        public IHocSinhRepository HocSinhRepository { get; set; }
        public IDiemHocSinhRepository DiemHocSinhRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IRefundRepository RefundRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }
        public IOrderDetailRepository OrderDetailRepository { get; set; }
        public IFundRepository FundRepository { get; set; }
        public IBlogRepository BlogRepository { get; set; }
        public IKhoaHocRepository KhoaHocRepository { get; set; }
        public IAddFundTransactionRepository AddFundTransactionRepository { get; set; }
        public UnitOfWork(GameStoreDbContext context,
            IProductRepository productRepository,
            IGiaoVienRepository giaoVienRepository,
            IHocSinhRepository hocsinhRepository,
            IDiemHocSinhRepository diemHocSinhRepository,
            IOrderRepository orderRepository,
            IRefundRepository refundRepository,
            IAdminRepository adminRepository,
            IOrderDetailRepository orderDetailRepository,
            IFundRepository fundRepository,
            IBlogRepository blogRepository,
            IAddFundTransactionRepository addFundTransactionRepository,
            IKhoaHocRepository khoaHocRepository
            )
        {
            Context = context;
            ProductRepository= productRepository;
            GiaoVienRepository= giaoVienRepository;
            HocSinhRepository = hocsinhRepository;
            DiemHocSinhRepository = diemHocSinhRepository;
            OrderRepository = orderRepository;
            RefundRepository = refundRepository;
            AdminRepository = adminRepository;
            OrderDetailRepository = orderDetailRepository;
            FundRepository = fundRepository;
            BlogRepository = blogRepository;
            AddFundTransactionRepository = addFundTransactionRepository;
            KhoaHocRepository = khoaHocRepository;
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
        public void SaveChange()
        {
            Context.SaveChanges();
        }
    }
}
