using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static Homework_Endpoint.Todo;

namespace Homework_Endpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintUsersAsync();
            Console.ReadLine();

            PrintTodosAsync();
            Console.ReadLine();
        }

        private const string BASE_URL = "https://jsonplaceholder.typicode.com/";
        //private const string API_KEY = "mLUbG3hmcqhyz9ARaH55RYXvpo3p2vNG"; // в кожного свій (зареєструвати)
        private const string USER_ENDPOINT = "users";
        private const string TODO_ENDPOINT = "todos";

        public static async Task<List<User>> GetUsersAsync() //считування з сайта
        {
            var users = new List<User>();
            var url = BASE_URL + String.Format(USER_ENDPOINT);
            using (var user = new HttpClient())
            {
                var responce = await user.GetAsync(url);
                var json = await responce.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            return users;
        }

        static async void PrintUsersAsync() //вивід на екран users
        {
            var users = await GetUsersAsync();
            foreach (User item in users)
            {
                Console.WriteLine(item.name, item.username);
            }
            File.WriteAllText("WriteUser.txt", $"{users}");
        }

        public static async Task<List<Todo>> GetTodosAsync() //считування з сайта
        {
            var todos = new List<Todo>();
            var url = BASE_URL + String.Format(TODO_ENDPOINT);
            using (var todo = new HttpClient())
            {
                var responce = await todo.GetAsync(url);
                var json = await responce.Content.ReadAsStringAsync();
                todos = JsonConvert.DeserializeObject<List<Todo>>(json);
            }
            return todos;
        }

        static async void PrintTodosAsync() //вивід на екран todos
        {
            var todos = await GetTodosAsync();
            foreach (Todo item in todos)
            {
                Console.WriteLine($"{item.id.ToString()} {item.title.ToString()} {item.userId.ToString()}");
            }
        }
    }


}
