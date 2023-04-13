using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_BLL.ViewModel;
using APP_ClientMVC.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace APP_ClientMVC.Services
{
    public class AuthorService : IAuthorService
    {
        const string apiurl = "https://localhost:7230";

        private readonly IMapper _mapper;

        public AuthorService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<Author> AddAuthor(HttpContext httpContext, Author author)
        {
            var accessToken = httpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7230/api/Authors", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<Author>(apiResponse);
                }
            }
            return author;
        }

        public async Task<bool> DeleteAuthor(HttpContext HttpContext, int authorId)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.DeleteAsync($"{apiurl}/api/Authors/{authorId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{apiResponse}");
                }
            }

            return true;
        }

        public async Task<Author> GetAuthorToUpdate(HttpContext HttpContext, int id)
        {
            Author author = new Author();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiurl}/api/Authors/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<Author>(apiResponse);
                }
            }

            return author;
        }

        public async Task<List<Author>> GetAuthors(HttpContext HttpContext)
        {
            List<Author> authorList = new List<Author>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync($"{apiurl}/api/Authors"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    authorList = JsonConvert.DeserializeObject<List<Author>>(apiResponse);
                }
            }

            return authorList;
        }

        public async Task<bool> UpdateAuthor(HttpContext httpContext, UpdateAuthorRequest author, int id)
        {
            var accessToken = httpContext.Session.GetString("JWToken");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{apiurl}/api/Authors/{id}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();                    
                    var receivedAuthor = JsonConvert.DeserializeObject<UpdateAuthorRequest>(apiResponse);
                }
            }
            return true;
        }

        public async Task<DetailsAuthorViewModel> GetAuthorDetails(HttpContext httpContext, int id)
        {
            DetailsAuthorDTO authorDetails = new DetailsAuthorDTO();

            var accessToken = httpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync($"{apiurl}/api/Authors/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    authorDetails = JsonConvert.DeserializeObject<DetailsAuthorDTO>(apiResponse);
                }
            }

            var authorDetailsViewModel = _mapper.Map<DetailsAuthorViewModel>(authorDetails);
            return authorDetailsViewModel;
        }
    }
}
