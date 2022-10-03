using CarSalesAgency.DataAccess.Repository.IRepository;
using CarSalesAgency.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarSalesAgency.Utility;
using Microsoft.AspNetCore.Authorization;

namespace CarSalesAgencyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CarController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CarController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Upsert(int? Id)
        {
            CarVm carVm = new()
            {
                Car = new(),
                BrandList = _UnitOfWork.Brand.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (Id == null || Id == 0)
            {
                //Create product
                return View(carVm);
            }
            else
            {
                //Update product
                carVm.Car = _UnitOfWork.Car.GetFirstOrDefault(u => u.Id == Id);
                return View(carVm);
            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CarVm obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate new file name
                    string FileName = Guid.NewGuid().ToString();
                    //find the location where the files should be uploaded
                    var uploads = Path.Combine(wwwRootPath, @"images\cars");
                    //keep same extension
                    var extension = Path.GetExtension(file.FileName);

                    //update the image - Check if there is an image
                    if (obj.Car.ImgUrl != null)
                    {
                        //old image path
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Car.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //copy the file uploaded inside the product folder
                    using (var fileStreams = new FileStream(Path.Combine(uploads, FileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    //what we will save in the DB
                    obj.Car.ImgUrl = @"\images\cars\" + FileName + extension;
                }
                if (obj.Car.Id == 0)
                {
                    _UnitOfWork.Car.Add(obj.Car);

                }
                else
                {
                    _UnitOfWork.Car.Update(obj.Car);
                }
                _UnitOfWork.Save();
                TempData["success"] = "car created successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _UnitOfWork.Car.GetAll(includeProperties: "Brand");
            return Json(new { data = productList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Car.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, Obj.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _UnitOfWork.Car.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
