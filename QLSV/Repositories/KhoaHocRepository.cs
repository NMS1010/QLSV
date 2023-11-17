using QLSV.InterfacesRepository;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Repositories
{
    public class FundRepository : GameStoreRepository<Fund>, IFundRepository
    {
        public FundRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
