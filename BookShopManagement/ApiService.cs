using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookShopManagement.Models;

namespace BookShopManagement
{
    internal class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7141/api/Books") // Update with your actual API URL
            };
        }

        // Get all books
        public async Task<List<Book>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync("books");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Book>>(jsonResponse);
        }

        // Get a single book by ID
        public async Task<Book> GetBookAsync(int id)
        {
            var response = await _httpClient.GetAsync($"books/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Book>(jsonResponse);
        }

        // Create a new book
        public async Task CreateBookAsync(Book book)
        {
            var jsonRequest = JsonConvert.SerializeObject(book);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("books", content);
            response.EnsureSuccessStatusCode();
        }

        // Update an existing book
        public async Task UpdateBookAsync(Book book)
        {
            var jsonRequest = JsonConvert.SerializeObject(book);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"books/{book.BId}", content);
            response.EnsureSuccessStatusCode();
        }

        // Delete a book by ID
        public async Task DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"books/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
