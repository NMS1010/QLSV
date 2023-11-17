using QLSV.Interfaces;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Repositories
{
    public class BlogRepository : GameStoreRepository<Blog>, IBlogRepository
    {
        public BlogRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
