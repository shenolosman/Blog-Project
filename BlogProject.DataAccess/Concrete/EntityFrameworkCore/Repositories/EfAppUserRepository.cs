using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {
        private readonly DatabaseContext _context;
        public EfAppUserRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
