using Dapper;
using QLSV.Enums;
using QLSV.Interfaces;
using QLSV.Models;
using QLSV.ModelViews;
using QLSV.OtpModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace QLSV.Repositories
{
    public class ProductRepository:GameStoreRepository<KhoaHoc>,IProductRepository
    {

        public ProductRepository(GameStoreDbContext context):base(context)
        {

        }

        public List<KhoaHoc> GetProductByName(string name)
        {
            return this.GetAll().Where(t => t.course_name.ToLower().Contains(name)).ToList();
        }
        public List<gameRefund> listGameRefund(int Userid)
        {
            var query = @"select distinct productid,p.Name,Image from 
                        [order] o,orderdetail od,Product p
                        where o.id=od.id and od.ProductID=p.Id and datediff(day,datepurchase,getdate())<=7 and UserID=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", Userid);
            var result = Context.Database.GetDbConnection().Query<gameRefund>(query, parameter);
            return result.ToList();
        }

        public List<KhoaHoc> listProductDev(int id)
        {
            var query = @"select * from Product where Product.DevId = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var result = Context.Database.GetDbConnection().Query<KhoaHoc>(query, parameter);
            return result.ToList();
        }

        public List<KhoaHoc> listProductItem()
        {
            var query = @"select distinct Product.* from Product, Item where Product.Id = Item.ProductId";
            var result = Context.Database.GetDbConnection().Query<KhoaHoc>(query);
            return result.ToList();
        }
        public List<KhoaHoc> listProductItem(int id)
        {
            var query = @"select Product.* from Product, Item,Inventory where Product.Id = Item.ProductId and Inventory.ItemID = Item.Id and Inventory.UserID =@id
group by Product.AppId,Product.Id,Product.Name,Product.Overview,Product.Description,Product.ReleaseDate,Product.Price,Product.Image,Product.DevId,Product.Status";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var result = Context.Database.GetDbConnection().Query<KhoaHoc>(query, parameter);
            return result.ToList();
        }
        public List<KhoaHoc> listProductRelease()
        {
            var query = @"select * from Product where CAST(releasedate as date)=CAST(getdate() as date) and Status=1";
            var data = Context.Database.GetDbConnection().Query<KhoaHoc>(query);
            return data.ToList();
        }


        public Refund IsRefundGame(int UserId, int ProductId)
        {
            int Status = (int)RefundType.pending;
            var query = @"select top 1.* from Refund
                        where UserID=@userid and ProductID=@productid and Status=@status
                        order by DatePurchase desc";
            var parameters=new DynamicParameters();
            parameters.Add("userid", UserId);
            parameters.Add("productid", ProductId);
            parameters.Add("status", Status);
            var result=Context.Database.GetDbConnection().Query<Refund>(query,parameters).SingleOrDefault();
            if(result==null)
            {
                return null;
            }
            else
            {
                return result;
            }        
        }
    }
}
