using QLSV.Interfaces;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Repositories
{
    public class AdminRepository : GameStoreRepository<Admin>, IAdminRepository
    {
        public AdminRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
