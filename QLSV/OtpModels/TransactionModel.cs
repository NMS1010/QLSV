using QLSV.Models;
using System.Collections.Generic;

namespace QLSV.OtpModels
{
    public class TransactionRequest
    {
        public int MarketID { get; set; }
        public int buyerID { get; set; }    
        public int sellerID { get; set; }
        public int itemID { get; set; } 
        public int quantity { get; set; }
        public int totalprice { get; set; }

    }

    public class MarketOrder
    {
        public List<Order> order { get; set; }
        public List<AddFundTransaction> addFundTransaction { get; set; }
    }
}
