
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;


namespace Asp_.net_core_web_app__MVC_.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // ApplicationDbContext service already register in program.cs file, so directly referenced it and used by local variable.
        //private readonly ApplicationDbContext _db;        // we use this in repository instead of use in every controller.

        public readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)     // here we asking dependency inject to provide implementation of the i category repo. so we have to registerd service in program.cs file.
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (!ModelState.IsValid || obj == null)
            {
                return BadRequest();
            }

            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("name", "category Name is can not be test."); //key which is used in asp-for control.
            }

            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";  // use to notify that action is completed with message dialog.
            return RedirectToAction("Index", "Category");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            Category category = _unitOfWork.Category.GetValue(p => p.Id == id);  // find, Where etc way ...
            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)  // add debug and check whether id is populated or not other wise new record will be created.
        {
            if (!ModelState.IsValid || obj == null)
            {
                return BadRequest();
            }

            _unitOfWork.Category.update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index", "Category");  // if in the same controller no need to write controller name.
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            Category category = _unitOfWork.Category.GetValue(p => p.Id == id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)    // here you can also get category obj as well as edit post method.
        {
            if (id == null || id == 0) { return BadRequest(); }

            Category category = _unitOfWork.Category.GetValue(p => p.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
