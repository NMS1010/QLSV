using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Models
{
    public class HocSinh : BaseEntity
    {
        public string Gmail { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }
        public DateTime date_of_birth { get; set; }
        public string Salt { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public List<DiemHocSinh> DiemHocSinhs { get; set; }
        public List<Order> Orders { get; set; }
        public List<Refund> Refunds { get; set; }
        public List<AddFundTransaction> AddFundTransactions { get; set; }
        public List<LichHoc> LichHocs { get; set; }
        public List<File> Files { get; set; }
        public List<ThongBao> ThongBaos { get; set; }
    }
}