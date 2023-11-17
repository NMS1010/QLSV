using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Models
{
    public class LichKhoaHoc : BaseEntity
    {
        [ForeignKey("KhoaHoc")]
        public int IdKhoaHoc { get; set; }

        public string ThoiGian { get; set; }
        public string Thu { get; set; }

        public KhoaHoc KhoaHoc { get; set; }
    }
}