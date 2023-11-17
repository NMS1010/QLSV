using System;
using System.Collections.Generic;

namespace QLSV.Models
{
    public class Order : BaseEntity
    {
        public int UserID { get; set; }
        public DateTime DatePurchase { get; set; }
        public decimal TotalPrice { get; set; }
        public HocSinh User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Refund> Refunds { get; set; }
    }
}