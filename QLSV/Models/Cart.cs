using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Models
{
    public class Cart : BaseEntity
    {
        public KhoaHoc product { get; set; }
    }
}