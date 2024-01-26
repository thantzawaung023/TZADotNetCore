using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using TZADotNetCore.RestApi.Models;

namespace TZADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "TZADotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };

        [HttpGet]
        public IActionResult GetBlogs()
        {
            String query = @"SELECT Blog_Id
                  ,Blog_Title
                  ,[Blog_Author]
                  ,Blog_Content
              FROM Tbl_Blog";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            String query = @"SELECT [Blog_Id]
              ,[Blog_Title]
              ,[Blog_Author]
              ,[Blog_Content]
          FROM [dbo].[Tbl_Blog] Where Blog_Id = @Blog_ID";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreatBlog(BlogDataModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
            VALUES ( @Blog_Title
           ,@Blog_Author
           ,@Blog_Content )";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            #region get by id

            String query = @"SELECT [Blog_Id]
              ,[Blog_Title]
              ,[Blog_Author]
              ,[Blog_Content]
          FROM [dbo].[Tbl_Blog] Where Blog_Id = @Blog_ID";
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            #endregion

            #region Check Required Fields
            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog Title is require");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog Author is require");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog Content is require");
            }
            #endregion
            blog.Blog_Id = id;
            string UpdateQuery = @"UPDATE [dbo].[Tbl_Blog]
               SET [Blog_Title] = @Blog_Title
                  ,[Blog_Author] =@Blog_Author
                  ,[Blog_Content] =@Blog_Content
             WHERE Blog_Id = @Blog_Id";

            int result = db.Execute(UpdateQuery, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            #region get by id
            String query = @"SELECT [Blog_Id]
              ,[Blog_Title]
              ,[Blog_Author]
              ,[Blog_Content]
          FROM [dbo].[Tbl_Blog] Where Blog_Id = @Blog_ID";
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            #endregion

            #region check required fields
            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += @"[blog_Title] = @Blog_Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += @"[blog_Author] = @Blog_Author, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += @"[blog_Content] = @Blog_Content, ";
            }
            if (conditions.Length == 0 )
            {
                return BadRequest("Invalid Request");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.Blog_Id = id;

            #endregion

            string UpdateQuery = $@"UPDATE [dbo].[Tbl_Blog]
               SET {conditions}
             WHERE Blog_Id = @Blog_Id";

            int result = db.Execute(UpdateQuery, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogDataModel { Blog_Id = id });

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

    }
}
