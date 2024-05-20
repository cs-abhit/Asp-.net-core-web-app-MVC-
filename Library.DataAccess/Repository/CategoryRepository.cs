using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)  // repository class need applicationDBContext when create a obj (call constructor),
                                                                      // in dependency inject it is provided at run tine but here we need to pass from here to base class. 
        {
            _db = db;
        }

        public void update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
