using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Models
{
    public class File:BaseEntity
    {
        public string name { get; set; }
        public int IdHocSinh { get; set; }
        public int IdGiaoVien { get; set; }
        public virtual HocSinh HocSinh { get; set; }
        public virtual GiaoVien GiaoVien { get; set; }
    }
}
