using QLSV.Models;
using QLSV.ModelViews;
using System.Collections.Generic;

namespace QLSV.InterfacesRepository
{
    public interface IOrderDetailRepository:IGameStoreRepository<OrderDetail>
    {
        List<XemDonHang> getorder(int id);
    }
}
