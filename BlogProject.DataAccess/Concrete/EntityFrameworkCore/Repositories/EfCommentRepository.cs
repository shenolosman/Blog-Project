using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal

    {
        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(blogId, parentId, result);
            return result;
        }

        private async Task GetComments(int blogId, int? parentId, List<Comment> result)
        {
            await using var context = new DatabaseContext();
            var comments = await context.Comments.Where(x => x.BlogId == blogId && x.ParentCommentId == parentId)
                .OrderByDescending(x => x.PostedTime).ToListAsync();

            if (comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    if (comment.SubComments == null)
                    {
                        comment.SubComments = new List<Comment>();
                    }
                    //recursive method to add whole tree with sub comments
                    await GetComments(comment.BlogId, comment.Id, comment.SubComments);

                    if (!result.Contains(comment))
                    {
                        result.Add(comment);
                    }
                }
            }
        }
    }
}
