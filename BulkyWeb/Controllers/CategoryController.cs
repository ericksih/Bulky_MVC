using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString()) // in case there is cannot same value on Name and display order
            {
                ModelState.AddModelError("name", "The display order cannot match the Name.");
            }

            if(obj.Name != null && obj.Name.ToLower() == "test") // in case we can accept input test
            {
                ModelState.AddModelError("name", "test is invalid value");
            }

            if(ModelState.IsValid)
            {
            _db.Categories.Add(obj); // what we doing is added category obj to categories table
            _db.SaveChanges(); // then save it to db.
                TempData["success"] = "Category created Successfully";
            return RedirectToAction("Index"); // go to index is meaning excecute to render list category
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

            Category? categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost] // for update
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) // in case there is cannot same value on Name and display order
            {
                ModelState.AddModelError("name", "The display order cannot match the Name.");
            }

            if (obj.Name != null && obj.Name.ToLower() == "test") // in case we can accept input test
            {
                ModelState.AddModelError("name", "test is invalid value");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); // what we doing is added category obj to categories table
                _db.SaveChanges(); // then save it to db.
                TempData["success"] = "Category updated Successfully";

                return RedirectToAction("Index"); // go to index is meaning excecute to render list category
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

            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")] // for update
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
            return NotFound();
            }
                _db.Categories.Remove(obj); 
                _db.SaveChanges(); // then save it to db.
            TempData["success"] = "Category deleted Successfully";

            return RedirectToAction("Index"); // go to index is meaning excecute to render list category
        }
    }
}
