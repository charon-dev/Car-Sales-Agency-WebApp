using CarSalesAgency.DataAccess.Repository.IRepository;
using CarSalesAgency.Models;
using CarSalesAgency.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesAgencyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class BrandController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public BrandController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
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
            Brand brand = new();
            if (Id == null || Id == 0)
            {
                //Create brand
                return View(brand);
            }
            else
            {
                //Update product
                brand = _UnitOfWork.Brand.GetFirstOrDefault(u => u.Id == Id);
                return View(brand);
            }
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Brand obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate new file name
                    string FileName = Guid.NewGuid().ToString();
                    //find the location where the files should be uploaded
                    var uploads = Path.Combine(wwwRootPath, @"images\brands");
                    //keep same extension
                    var extension = Path.GetExtension(file.FileName);

                    //update the image - Check if there is an image
                    if (obj.ImgUrl != null)
                    {
                        //old image path
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImgUrl.TrimStart('\\'));
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
                    obj.ImgUrl = @"\images\brands\" + FileName + extension;
                }
                if (obj.Id == 0)
                {
                    _UnitOfWork.Brand.Add(obj);

                }
                else
                {
                    _UnitOfWork.Brand.Update(obj);
                }
                _UnitOfWork.Save();
                TempData["success"] = "brand created successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var brandtList = _UnitOfWork.Brand.GetAll();
            return Json(new { data = brandtList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Brand.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, Obj.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _UnitOfWork.Brand.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
