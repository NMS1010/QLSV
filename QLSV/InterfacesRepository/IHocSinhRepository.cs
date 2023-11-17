using QLSV.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLSV.Interfaces
{
    public interface IHocSinhRepository:IGameStoreRepository<HocSinh>
    {
        void updateBalance(int userID, decimal price, int type);
        HocSinh FindByEmail(string email);
        List<HocSinh> listhocsinh(int id);
        Task<(byte[], string, string)> DownloadFile(string FileName);
        List<HocSinh> listhocsinhdamua();
    }
}
