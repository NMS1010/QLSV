using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLSV.Models
{
    public class KhoaHoc : BaseEntity
    {
        public string course_name { get; set; }
        public string description { get; set; }
        public int gia { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string Image { get; set; }
        public int? IdGiaoVien { get; set; }
        public GiaoVien GiaoVien { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Refund> Refunds { get; set; }
        public List<DiemHocSinh> DiemHocSinhs { get; set; }

        public List<LichKhoaHoc> LichKhoaHocs { get; set; }
        public List<FileKhoaHoc> FileKhoaHocs { get; set; }
        public List<ThongBaoKhoaHoc> ThongbaoKhoaHocs { get; set; }
    }
}