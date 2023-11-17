using System;

namespace QLSV.Models
{
    public class LichHoc:BaseEntity
    {
        public int IdHocSinh { get; set; }
        public int IdGiaoVien { get; set; }
        public DateTime thoiGian { get; set; }
        public virtual HocSinh HocSinh { get; set; }
        public virtual GiaoVien GiaoVien { get; set; }
    }
}
