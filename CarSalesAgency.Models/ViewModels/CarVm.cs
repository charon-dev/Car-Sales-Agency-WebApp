using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesAgency.Models.ViewModels
{
    public class CarVm
    {       
        public Car Car { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BrandList { get; set; }
       
    }
}
