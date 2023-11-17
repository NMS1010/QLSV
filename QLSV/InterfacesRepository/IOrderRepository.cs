using QLSV.Models;
using System.Collections.Generic;

namespace QLSV.Interfaces
{
    public interface IOrderRepository:IGameStoreRepository<Order>
    {
        Order createOrder(int userID,List<Cart> productPurcahse);
        int orderID(int userID);
    }
}
