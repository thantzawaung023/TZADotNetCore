using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZADotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TZADotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        string _blogEndpoint = "https://localhost:7180/api/blog";

        public async Task RunAsync()
        {
            await ReadAsync();
            /*     await EditAsync(5);
                 await EditAsync(1000);*/
            await UpdateAsync(5, new BlogDataModel() { Blog_Title = "testing1", Blog_Author="testing2",Blog_Content ="testing3"}) ;
            await PatchAsync(6, new BlogDataModel() { Blog_Title = "String Testing 1" });
            await DeleteAsync(7);
        }

        private async Task ReadAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_blogEndpoint);
            if (response.IsSuccessStatusCode)
            {
                String jsonString = await response.Content.ReadAsStringAsync();
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonString)!;
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                    Console.WriteLine("---------------------");
                }

            }
        }

        private async Task EditAsync(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                String jsonString = await response.Content.ReadAsStringAsync();
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonString)!;


                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine("---------------------");


            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(_blogEndpoint, httpContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        private async Task UpdateAsync(int id, BlogDataModel blog)
        {

            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync($"{_blogEndpoint}/{id}", httpContent);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        private async Task PatchAsync(int id, BlogDataModel blog)
        {
           
            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PatchAsync($"{_blogEndpoint}/{id}", httpContent);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        private async Task DeleteAsync(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"{_blogEndpoint}/{id}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

    }
}
