using System;

namespace QLSV.Models
{
    public class Refund:BaseEntity
    {
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime DateCreate { get; set; }
        public HocSinh User { get; set; }
        public KhoaHoc Product { get; set; }
        public Order Order { get; set; }
    }
}
