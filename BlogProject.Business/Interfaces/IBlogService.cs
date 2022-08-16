using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Interfaces
{
    public interface IBlogService : IGenericService<Blog>
    {
        Task<List<Blog>> GelAllSortedByPostedTimeAsync();
    }
}
