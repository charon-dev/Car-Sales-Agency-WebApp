using CarSalesAgency.DataAccess.Data;
using CarSalesAgency.DataAccess.Repository.IRepository;
using CarSalesAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesAgency.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? payementStatus = null)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                //Update Order status
                orderFromDb.OrderStatus = orderStatus;
                if (payementStatus != null)
                {
                    //Update Payment status
                    orderFromDb.PaymentStatus = payementStatus;
                }
            }
        }
        public void UpdateStripePayementID(int id, string sessionId, string payementItentId)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            orderFromDb.PayementDate = DateTime.Now;
            orderFromDb.SessionId = sessionId;
            orderFromDb.PayementIntentId = payementItentId;

        }
    }
}
