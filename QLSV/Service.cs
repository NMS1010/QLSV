using QLSV.InterfacesService;

namespace QLSV
{
    public class Service : IService
    {
        public IOrderService OrderService { get; }
        public IRefundService RefundService { get; }
        public IHocSinhService HocSinhService { get; }
        public Service(
            IOrderService orderService, 
            IRefundService refundService, 
            IHocSinhService hocSinhService
            )
        {
            OrderService = orderService;
            RefundService = refundService;
            HocSinhService = hocSinhService;
        }
    }
}
