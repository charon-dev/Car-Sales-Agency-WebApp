using CarSalesAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesAgency.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCarts>
    {
        int IncrementCount(ShoppingCarts shoppingCart, int count);
        int DecrementCount(ShoppingCarts shoppingCart, int count);

    }
}
