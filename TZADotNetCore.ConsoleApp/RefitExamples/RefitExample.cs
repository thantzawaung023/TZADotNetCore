using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZADotNetCore.ConsoleApp.Models;

namespace TZADotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7180");

        public async Task Run()
        {
            await Read();
            /*await Edit(6);*/
            /* await Edit(25);
             await Create("Test String 1", "Test String 2", "Test String 3");*/
            await Update(5, new BlogDataModel() { Blog_Title = "Some 1", Blog_Author = "Some 2", Blog_Content = "Some 3" });
 /*           await Update(4, new BlogDataModel() { Blog_Title = "Some 1", Blog_Author = "Some 2" });*/
            await Patch(14, new BlogDataModel() { Blog_Title = "Some 1", Blog_Author = "Some 3" });
            await Edit(14);
            await Edit(5);
            await Delete(19);
        }

        private async Task Read()
        {

            List<BlogDataModel> lst = await _blogApi.GetBlogs();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine("---------------------");
            }

        }

        private async Task Edit(int id)
        {
            try
            {
                BlogDataModel item = await _blogApi.GetBlog(id);
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine("---------------------");
            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());

            }


        }

        private async Task Create(String title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            String message = await _blogApi.CreateBlog(blog);

            Console.WriteLine(message);

        }

        private async Task Update(int id, BlogDataModel blog)
        {
            try
            {
                String message = await _blogApi.UpdateBlog(id, blog);
                Console.WriteLine(message);

            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());

            }

        }

        private async Task Patch(int id, BlogDataModel blog)
        {
            try
            {
                String message = await _blogApi.PatchBlog(id, blog);
                Console.WriteLine(message);

            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());

            }

        }

        private async Task Delete(int id)
        {
            try
            {
                String message = await _blogApi.DeleteBlog(id);
                Console.WriteLine(message);

            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());

            }

        }

    }
}
