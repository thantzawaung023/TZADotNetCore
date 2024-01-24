using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZADotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {


        public void Run()
        {
            /* Read();*/
            /*Edit(1);
            Edit(19);*/
            /* Create("Test Title", "Test Author", "Test Content");*/
            /*Update(1, "Test Title", "Test Author", "Test Content");*/
            Delete(2);
        }

        private void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "TZADotNetCore",
                UserID = "sa",
                Password = "sa@123",
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

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
            Console.WriteLine("Connection Close");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"Id      => {row["Blog_Id"]}");
                Console.WriteLine($"Title   => {row["Blog_Title"]}");
                Console.WriteLine($"Author  => {row["Blog_Author"]}");
                Console.WriteLine($"Content => {row["Blog_Content"]}");
                Console.WriteLine("--------------------------------------");

            }
        }

        private void Edit(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "TZADotNetCore",
                UserID = "sa",
                Password = "sa@123",
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

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
            Console.WriteLine("Connection Close");

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            DataRow dataRow = dataTable.Rows[0];

            Console.WriteLine($"Id      => {dataRow["Blog_Id"]}");
            Console.WriteLine($"Title   => {dataRow["Blog_Title"]}");
            Console.WriteLine($"Author  => {dataRow["Blog_Author"]}");
            Console.WriteLine($"Content => {dataRow["Blog_Content"]}");
            Console.WriteLine("--------------------------------------");


        }

        private void Create(string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "TZADotNetCore",
                UserID = "sa",
                Password = "sa@123",
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            int result = command.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection Close");

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);



        }

        private void Update(int id, string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "TZADotNetCore",
                UserID = "sa",
                Password = "sa@123",
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [Blog_Title] = @Blog_Title
                  ,[Blog_Author] =@Blog_Author
                  ,[Blog_Content] =@Blog_Content
             WHERE Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            int result = command.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection Close");

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);

        }
        private void Delete(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "TZADotNetCore",
                UserID = "sa",
                Password = "sa@123",
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            int result = command.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection Close");

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);

        }



    }
}
