using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private IGenericDal<Blog> _genericDal;
        private readonly IBlogDal _blogDal;

        public BlogManager(IGenericDal<Blog> genericDal, IBlogDal blogDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _blogDal = blogDal;
        }

        public async Task<List<Blog>> GelAllSortedByPostedTimeAsync()
        {
            return await _genericDal.GelAllAsync(x => x.PostedTime);
        }
    }
}
