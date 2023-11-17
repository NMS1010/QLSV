using QLSV.Models;
using QLSV.OtpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Interfaces
{
    public interface IDiemHocSinhRepository:IGameStoreRepository<DiemHocSinh>
    {
        List<DiemHocSinh> getDiemHocSinh(int id);
        void remove(int userID, int productID);
        void updateDiemHocSinh(int userID, List<Cart> cart);
    }
}
