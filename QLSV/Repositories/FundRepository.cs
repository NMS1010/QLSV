using QLSV.InterfacesRepository;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Repositories
{
    public class KhoaHocRepository : GameStoreRepository<KhoaHoc>, IKhoaHocRepository
    {
        public KhoaHocRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
