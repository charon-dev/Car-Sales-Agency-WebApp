using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesAgency.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCarts> ListCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
