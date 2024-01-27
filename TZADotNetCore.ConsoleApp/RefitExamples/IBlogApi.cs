using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZADotNetCore.ConsoleApp.Models;

namespace TZADotNetCore.ConsoleApp.RefitExamples
{
    public interface IBlogApi
    {

        [Get("/api/blog")]
        Task<List<BlogDataModel>> GetBlogs();

        [Get("/api/blog/{id}")]
        Task<BlogDataModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<String> CreateBlog(BlogDataModel blog);

        [Put("/api/blog/{id}")]
        Task<String> UpdateBlog(int id, BlogDataModel blog);

        [Patch("/api/blog/{id}")]
        Task<String> PatchBlog(int id,BlogDataModel blog);

        [Delete("/api/blog/{id}")]
        Task<String> DeleteBlog(int id);



    }
}
