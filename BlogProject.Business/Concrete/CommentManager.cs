using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly IGenericDal<Comment> _genericDal;

        public CommentManager(IGenericDal<Comment> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }
    }
}
