using QLSV.Models;

namespace QLSV.Interfaces
{
    public interface IGiaoVienRepository:IGameStoreRepository<GiaoVien>
    {
        GiaoVien getDev(string id);
    }
}
