using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Models
{
    public class ThongBaoKhoaHoc : BaseEntity
    {
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }

        [ForeignKey("KhoaHoc")]
        public int IdKhoaHoc { get; set; }

        public KhoaHoc KhoaHoc { get; set; }
    }
}