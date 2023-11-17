using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.ModelViews
{
    public class ItemModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ProductId { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int Quantity { get; set; }
    }
}
