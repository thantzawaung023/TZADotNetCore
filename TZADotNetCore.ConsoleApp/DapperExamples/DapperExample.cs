﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TZADotNetCore.ConsoleApp.Models;

namespace TZADotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "TZADotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };
        public void Run()
        {
            /*  Read();
              Edit(2);
              Edit(19);
              Create("Test Title", "Test Author", "Test Content");*/
            Update(5, "Test Title", "Test Author", "Test Content");
            Delete(11);

        }


        private void Read()
        {
            String query = @"SELECT Blog_Id
                  ,Blog_Title
                  ,[Blog_Author]
                  ,Blog_Content
              FROM Tbl_Blog";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();

            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine("---------------------");
            }


        }

        private void Edit(int id)
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
                Console.WriteLine("No Data Found.");
                return;
            }

            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
            Console.WriteLine("---------------------");


        }

        private void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
            VALUES ( @Blog_Title
           ,@Blog_Author
           ,@Blog_Content )";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content

            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);


        }

        private void Update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [Blog_Title] = @Blog_Title
                  ,[Blog_Author] =@Blog_Author
                  ,[Blog_Content] =@Blog_Content
             WHERE Blog_Id = @Blog_Id";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content

            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);

        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query,new BlogDataModel { Blog_Id = id});

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);

        }

    }
}
