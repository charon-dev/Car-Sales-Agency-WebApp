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
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private ApplicationDbContext _db;
        public CarRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Car obj)
        {
            _db.cars.Update(obj);
        }
    }
}
