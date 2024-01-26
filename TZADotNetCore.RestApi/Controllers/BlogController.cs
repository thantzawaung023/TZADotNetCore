using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TZADotNetCore.RestApi.Models;

namespace TZADotNetCore.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _dbContext = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogDataModel> lst = _dbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlogs(int pageSize, int pageNo)
        {

            // end row no = pageNO * pageSize ; 10
            //start row no = end row - page size +1 ;
            List<BlogDataModel> lst = _dbContext.Blogs.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            var rowCount = _dbContext.Blogs.Count();
            var pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return Ok(new { IsEndOfPage = pageCount <= pageNo, PageCount = pageCount, PageSize = pageSize, PageNo = pageNo, Data = lst });
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogDataModel item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreatBlog(BlogDataModel blog)
        {
            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();
            var message = result > 0 ? "Svaing Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            BlogDataModel item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }


            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog_Title is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog_Author is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog_Content is required.");
            }

            int result = _dbContext.SaveChanges();
            var message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            BlogDataModel item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }

            int result = _dbContext.SaveChanges();
            var message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            BlogDataModel item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();
            var message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

    }
}
