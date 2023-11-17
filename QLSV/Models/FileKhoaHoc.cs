using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Models
{
    public class FileKhoaHoc : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }

        [ForeignKey("KhoaHoc")]
        public int IdKhoaHoc { get; set; }

        public KhoaHoc KhoaHoc { get; set; }
    }
}