﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZADotNetCore.ConsoleApp.Models;

namespace TZADotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(2);
            //Edit(19);
            //Create("test title", "test author", "test content"); 
            Update(10, "Test Title", "Test Author", "Test Content");
            //Delete(13);
        }

        private void Read()
        {

            List<BlogDataModel> lst = _dbContext.Blogs.AsNoTracking().ToList();

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

            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
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

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content

            };

            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);

        }

        private void Update(int id, string title, string author, string content)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }


            item.Blog_Title = title;
            item.Blog_Author = author;
            item.Blog_Content = content;

            _dbContext.Update(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);

        }

        private void Delete(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            _dbContext.Remove(item);
            int result = _dbContext.SaveChanges();  

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);

        }
    }
}
