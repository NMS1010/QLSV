using QLSV.Models;

namespace QLSV.InterfacesService
{
    public interface IRefundService
    {
        Refund refund(int userID, int productID);
        void refundtoallUser();
    }
}
