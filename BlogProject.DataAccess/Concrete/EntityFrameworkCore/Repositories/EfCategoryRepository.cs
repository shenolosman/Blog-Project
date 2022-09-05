using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryDal
    {
        private readonly DatabaseContext _context;
        public EfCategoryRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllWithCategoryBlogsAsync()
        {
            return await _context.Categories.OrderByDescending(x => x.Id).Include(x => x.CategoryBlogs).ToListAsync();
        }
    }
}
