using Dapper;
using QLSV.Interfaces;
using QLSV.Models;
using Microsoft.EntityFrameworkCore;

namespace QLSV.Repositories
{
    public class GiaoVienRepository:GameStoreRepository<GiaoVien>,IGiaoVienRepository
    {
        public GiaoVienRepository(GameStoreDbContext context):base(context)
        {

        }
        public GiaoVien getDev(string id)
        {
            var query = @"select * from GiaoVien where email=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().QuerySingle<GiaoVien>(query, parameter);
            return data;
        }
    }
}
