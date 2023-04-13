using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_BLL.ViewModel;
using APP_ClientMVC.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace APP_ClientMVC.Services
{
    public class BookService : IBookService
    {
        const string apiurl = "https://localhost:7230";

        private readonly IMapper _mapper;

        public BookService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<AddBook> AddBook(HttpContext httpContext, AddBook addBook)
        {
            var accessToken = httpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(addBook), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{apiurl}/api/Books", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addBook = JsonConvert.DeserializeObject<AddBook>(apiResponse);
                }

            }

            return addBook;
        }

        public async Task DeleteBook(HttpContext httpContext, int bookId)
        {
            var accessToken = httpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.DeleteAsync($"{apiurl}/api/Books/{bookId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{apiResponse}");
                }
            }
        }

        public async Task<List<Book>> GetAllBooks(HttpContext HttpContext)
        {
            List<Book> productList = new List<Book>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync($"{apiurl}/api/Books"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    productList = JsonConvert.DeserializeObject<List<Book>>(apiResponse);
                }
            }

            return productList;
        }

        public async Task<DetailsBookViewModel> GetBookDetails(HttpContext httpContext, int bookId)
        {
            DetailsBookDTO bookDetails = new DetailsBookDTO();

            var accessToken = httpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync($"{apiurl}/api/Books/{bookId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bookDetails = JsonConvert.DeserializeObject<DetailsBookDTO>(apiResponse);
                }
            }

            var bookDetailsViewModel = _mapper.Map<DetailsBookViewModel>(bookDetails);
            return bookDetailsViewModel;
        }

        public async Task<DetailsBookViewModel> GetBookToUpdate(HttpContext HttpContext, int bookId)
        {
            DetailsBookDTO bookDetails = new DetailsBookDTO();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync($"{apiurl}/api/Books/{bookId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bookDetails = JsonConvert.DeserializeObject<DetailsBookDTO>(apiResponse);
                }
            }

            var bookDetailsViewModel = _mapper.Map<DetailsBookViewModel>(bookDetails);
            return bookDetailsViewModel;
        }

        public async Task<bool> UpdateBookRequest(HttpContext httpContext, UpdateBookRequest book)
        {
            var accessToken = httpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{apiurl}/api/Books/{book.Id}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var receivedAuthor = JsonConvert.DeserializeObject<UpdateAuthorRequest>(apiResponse);
                }
            }
            return true;
        }
    }
}
