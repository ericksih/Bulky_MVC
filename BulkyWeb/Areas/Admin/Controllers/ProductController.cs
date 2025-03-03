using Bulky.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
           

           

            if(ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj); 
                _unitOfWork.Save(); // then save it to db.
                TempData["success"] = "Product created Successfully";
            return RedirectToAction("Index"); 
            }

            return View();
        }

        // Edit controller
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound(); 
            }

            Product? obj = _unitOfWork.Product.Get(u=> u.Id == id);

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost] // for update
        public IActionResult Edit(Product obj)
        {
          

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save(); 
                TempData["success"] = "Product updated Successfully";

                return RedirectToAction("Index"); // go to index is meaning excecute to render list product
            }
            return View();
        }

        // Delete controller
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")] // for update
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
            return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted Successfully";

            return RedirectToAction("Index"); // go to index is meaning excecute to render list product
        }
    }
}
