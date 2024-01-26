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
        public IActionResult GetBlogs(int pageSize,int pageNo)
        {

            // end row no = pageNO * pageSize ; 10
            //start row no = end row - page size +1 ;
            List<BlogDataModel> lst = _dbContext.Blogs.Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
            var rowCount = _dbContext.Blogs.Count();
            var pageCount = rowCount/ pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return Ok(new {IsEndOfPage = pageCount <= pageNo,PageCount = pageCount, PageSize = pageSize,PageNo = pageNo,Data = lst});
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

        [HttpPut]
        public IActionResult UpdateBlog()
        {
            return Ok("Update");
        }


        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok("Patch");
        }


        [HttpDelete]
        public IActionResult DeleteBlog()
        {
            return Ok("delete");
        }

    }
}
