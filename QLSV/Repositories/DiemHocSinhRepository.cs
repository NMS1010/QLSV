using Dapper;
using QLSV.Interfaces;
using QLSV.Models;
using QLSV.OtpModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Repositories
{
    public class DiemHocSinhRepository:GameStoreRepository<DiemHocSinh>, IDiemHocSinhRepository
    {
        public DiemHocSinhRepository(GameStoreDbContext context) : base(context)
        {
            
        }
        public List<DiemHocSinh> getDiemHocSinh(int id)
        {
            var query = @"select * from HocSinh";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().Query<DiemHocSinh>(query,parameter);
            return data.ToList();
        }
        public void remove(int userID, int productID)
        {
            var query = @"delete from DiemHocSinh
                        where DiemHocSinh.ProductId=@productid and DiemHocSinh.UserID=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", userID);
            parameter.Add("productid", productID);
            Context.Database.GetDbConnection().Execute(query, parameter);
        }
      
        public void updateDiemHocSinh(int userID, List<Cart> cart)
        {
            List<DiemHocSinh> libraries = new List<DiemHocSinh>();
            foreach(var p in cart)
            {
                DiemHocSinh DiemHocSinh = new DiemHocSinh()
                {
                    IdHocSinh = userID,
                    IdKhoaHoc = p.product.Id
                };
                libraries.Add(DiemHocSinh);
            }
            this.BulkInsert(libraries);
        }
    }
}
