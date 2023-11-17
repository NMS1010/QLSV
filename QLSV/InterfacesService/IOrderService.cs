using QLSV.Models;
using System.Collections.Generic;

namespace QLSV.InterfacesService
{
    public interface IOrderService
    {
        Order createOrder(int userID, List<Cart> productPurchase);
    }
}
