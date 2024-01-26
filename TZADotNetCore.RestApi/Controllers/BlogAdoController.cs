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
    public class BlogAdoController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "TZADotNetCore",
            UserID = "sa",
            Password = "sa@123",

        };

        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            String query = @"SELECT Blog_Id
      ,Blog_Title
      ,[Blog_Author]
      ,Blog_Content
  FROM Tbl_Blog";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            connection.Close();
            List<BlogDataModel> lst = new List<BlogDataModel>();
            foreach (DataRow row in dataTable.Rows)
            {
                lst.Add(new BlogDataModel
                {
                    Blog_Id = (int) row["Blog_Id"],
                    Blog_Title = row["Blog_Title"].ToString(),
                    Blog_Author = row["Blog_Author"].ToString(),
                    Blog_Content = row["Blog_Content"].ToString()
                });
            }

            return Ok(lst);

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
  
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            String query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] Where Blog_Id = @Blog_ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();
     

            if (dataTable.Rows.Count == 0)
            {
                return NotFound("No Data Found.");
            }

            DataRow dataRow = dataTable.Rows[0];
            BlogDataModel item = new BlogDataModel() 
            { 
                Blog_Id = (int) dataRow["Blog_Id"],
                Blog_Title= dataRow["Blog_Title"].ToString(),
                Blog_Author = dataRow["Blog_Author"].ToString(),
                Blog_Content = dataRow["Blog_Content"].ToString()
            };

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreatBlog(BlogDataModel blog)
        {

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            int result = command.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            String query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] Where Blog_Id = @Blog_ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count == 0)
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

            string UpdateQuery = @"UPDATE [dbo].[Tbl_Blog]
               SET [Blog_Title] = @Blog_Title
                  ,[Blog_Author] =@Blog_Author
                  ,[Blog_Content] =@Blog_Content
             WHERE Blog_Id = @Blog_Id";
            SqlCommand UpdateCommand = new SqlCommand(UpdateQuery, connection);
            UpdateCommand.Parameters.AddWithValue("@Blog_Id", id);
            UpdateCommand.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            UpdateCommand.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            UpdateCommand.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            int result = UpdateCommand.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection Close");

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            String query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] Where Blog_Id = @Blog_ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count == 0)
            {
                return NotFound("No Data Found.");
            }

            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += $@"[blog_Title] = '{blog.Blog_Title}', ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += $@"[blog_Author] = '{blog.Blog_Author}', ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += $@"[blog_Content] = '{blog.Blog_Content}', ";
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            string UpdateQuery = $@"UPDATE [dbo].[Tbl_Blog]
               SET {conditions}
             WHERE Blog_Id = @Blog_Id";
            SqlCommand UpdateCommand = new SqlCommand(UpdateQuery, connection);
            UpdateCommand.Parameters.AddWithValue("@Blog_Id", id);
            int result = UpdateCommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            int result = command.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
    }
}
