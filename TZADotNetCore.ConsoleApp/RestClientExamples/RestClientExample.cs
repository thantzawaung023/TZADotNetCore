using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZADotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TZADotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {

        string _blogEndpoint = "https://localhost:7180/api/blog";

        public async Task RunAsync()
        {
            await ReadAsync();
            await EditAsync(20);
            await EditAsync(100);
            await UpdateAsync(5, new BlogDataModel() { Blog_Title = "testing3", Blog_Author = "testing3"});
            await PatchAsync(6, new BlogDataModel() { Blog_Content = "String Testing 5" });
            await DeleteAsync(20);
        }

        private async Task ReadAsync()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content!;
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
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                String jsonString = response.Content!;
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonString)!;

                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine("---------------------");
            }
            else
            {
                Console.WriteLine(response.Content);
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

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
            request.AddJsonBody(blog);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }

        private async Task UpdateAsync(int id, BlogDataModel blog)
        {

            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            request.AddJsonBody(blog);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

        }

        private async Task PatchAsync(int id, BlogDataModel blog)
        {

            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Patch);
            request.AddJsonBody(blog);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

        }

        private async Task DeleteAsync(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

        }
    }
}
