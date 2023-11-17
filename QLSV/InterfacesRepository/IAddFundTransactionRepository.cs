﻿using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Interfaces
{
    public interface IAddFundTransactionRepository : IGameStoreRepository<AddFundTransaction>
    {
        List<AddFundTransaction> GetFundTransactions(int id);
    }
}
