using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesAgency.Models
{
    public class ShoppingCarts
    {
        public int Id { get; set; }
        [Range(1, 1000, ErrorMessage = "Pleas enter a value between 1 and 1000")]
        public int Count { get; set; }        
        [NotMapped]//Property that will be not mapped to Db
        public double Price { get; set; }

        public int CarId { get; set; }
        [ForeignKey("CarId")]
        [ValidateNever]
        public Car Car { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
