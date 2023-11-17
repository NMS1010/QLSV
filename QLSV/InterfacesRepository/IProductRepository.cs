using QLSV.Models;
using QLSV.ModelViews;
using QLSV.OtpModels;
using System.Collections.Generic;

namespace QLSV.Interfaces
{
    public interface IProductRepository:IGameStoreRepository<KhoaHoc>
    {
        List<KhoaHoc> GetProductByName(string name);
        List<KhoaHoc> listProductDev(int id);
        List<KhoaHoc> listProductItem();
        List<KhoaHoc> listProductItem(int id);
        List<KhoaHoc> listProductRelease();
        Refund IsRefundGame(int UserId, int ProductId);
    }
}
