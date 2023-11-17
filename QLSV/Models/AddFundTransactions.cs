using System;
using System.Web;

namespace QLSV.Models
{
    public class AddFundTransaction : BaseEntity
    {
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public int FundId { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreateOn { get; set; }
        public HocSinh User { get; set; }
        public Fund Fund { get; set; }
    }
}