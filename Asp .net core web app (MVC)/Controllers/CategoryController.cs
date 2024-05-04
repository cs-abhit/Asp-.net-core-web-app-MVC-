using Asp_.net_core_web_app__MVC_.Data;
using Asp_.net_core_web_app__MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Http.ModelBinding;

namespace Asp_.net_core_web_app__MVC_.Controllers
{
    public class CategoryController : Controller
    {
        // ApplicationDbContext service already register in program.cs file, so directly referenced it and used by local variable.
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
            if (!ModelState.IsValid  || obj == null)
            {
                return BadRequest();
            }

            if(obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("name", "category Name is can not be test."); //key which is used in asp-for control.
            }

            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";  // use to notify that action is completed with message dialog.
            return RedirectToAction("Index","Category");
        }


        public  IActionResult Edit(int? id) 
        {   
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            Category category = _db.Categories.FirstOrDefault(p => p.Id == id);  // find, Where etc way ...
            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)  // add debug and check whether id is populated or not other wise new record will be created.
        {
            if (!ModelState.IsValid || obj == null)
            {
                return BadRequest();
            }

            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index", "Category");  // if in the same controller no need to write controller name.
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            Category category = _db.Categories.Find(id); 
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)    // here you can also get category obj as well as edit post method.
        {
            if(id == null || id == 0) { return BadRequest(); }

            Category category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
