using System;

namespace QLSV.Models
{
    public class ThongBao : BaseEntity
    {
        public int IdHocSinh { get; set; }
        public int IdGiaoVien { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public virtual HocSinh HocSinh { get; set; }
        public virtual GiaoVien GiaoVien { get; set; }
    }
}