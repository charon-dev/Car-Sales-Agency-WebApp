using CarSalesAgency.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesAgency.DataAccess.Data
{
    //Object of Dbcontext(ApplicationDbContext)-using that we will be able to make connection to the database
    //IdentityDbContext in order to find ApplicationDbContext on DDL of Scaffloding identity
    public class ApplicationDbContext:IdentityDbContext
    {
        //Line of configuration(General syntax that is needed to establish the connection with entity)
        //The parameters should go to the base class(Dbcontext)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Car> cars { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<ShoppingCarts> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
