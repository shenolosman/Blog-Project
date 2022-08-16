using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryDal
    {
    }
}
