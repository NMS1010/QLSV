using QLSV.InterfacesService;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLSV.Services
{
    public class OrderService : IOrderService
    {
        public Order createOrder(int userID, List<Cart> productPurchase)
        {
            List<OrderDetail> detail = new List<OrderDetail>();
            foreach (var p in productPurchase)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductID = p.product.Id,
                    Price = p.product.gia
                };
                detail.Add(orderDetail);
            }
            Order order = new Order()
            {
                UserID = userID,
                DatePurchase = DateTime.Now,
                TotalPrice = productPurchase.Sum(t => t.product.gia),
                OrderDetails = detail
            };
            return order;
        }
    }
}
