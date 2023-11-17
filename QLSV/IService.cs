using QLSV.InterfacesService;

namespace QLSV
{
    public interface IService
    {
        IOrderService OrderService { get; }
        IRefundService RefundService { get; }
        IHocSinhService HocSinhService { get; }
    }
}
