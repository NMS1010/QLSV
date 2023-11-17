using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.ModelViews
{
    public class CartItem
    {
        public KhoaHoc product { get; set; }
        public int amount { get; set; }
    }
}
