using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CarSalesAgency.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        [Required]
        public int PlacesNumber { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        [ValidateNever]
        public Brand Brand { get; set; }
    }
}
