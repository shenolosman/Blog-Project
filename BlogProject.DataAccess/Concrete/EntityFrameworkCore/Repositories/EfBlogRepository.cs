﻿using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogDal
    {
        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            await using var context = new DatabaseContext();
            var blogs = context.Blogs.Join(context.CategoryBlogs, b => b.Id, cb => cb.BlogId, (blog, categoryBlog) => new
            {
                blog = blog,
                categoryBlog = categoryBlog
            }).Where(x => x.categoryBlog.CategoryId == categoryId).Select(x => new Blog
            {
                AppUser = x.blog.AppUser,
                AppUserId = x.blog.AppUserId,
                CategoryBlogs = x.blog.CategoryBlogs,
                Comments = x.blog.Comments,
                Id = x.blog.Id,
                ImagePath = x.blog.ImagePath,
                Description = x.blog.Description,
                PostedTime = x.blog.PostedTime,
                ShortDescription = x.blog.ShortDescription,
                Title = x.blog.Title,
            });
            return await blogs.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync(int blogId)
        {
            await using var context = new DatabaseContext();
            return await context.Categories.Join(context.CategoryBlogs, c => c.Id, cb => cb.CategoryId, (category, categoryBlog) =>
                   new
                   {
                       category = category,
                       categoryBlog = categoryBlog

                   }).Where(x => x.categoryBlog.BlogId == blogId).Select(x => new Category
                   {
                       Id = x.category.Id,
                       Name = x.category.Name
                   }).ToListAsync();
        }

        public async Task<List<Blog>> GetLastFiveAsync()
        {
            await using var context = new DatabaseContext();
            return await context.Blogs.OrderByDescending(x => x.PostedTime).Take(5).ToListAsync();
        }
    }
}
