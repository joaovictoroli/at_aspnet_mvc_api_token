using APP_BLL.Entities;
using APP_BLL.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.Text;

namespace APP_ClientMVC.Controllers
{
    public class AuthController : Controller
    {
        const string apiurl = "https://localhost:7230";

        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDetails userDetails)
        {
            using (var httpClient = new HttpClient())
            {

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7230/api/Auth/Register", stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Incorrect UserId or Password!";
                    }
                    else
                    {
                        return Redirect("~/Auth/Login");
                    }
                }

                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCredentials loginCredentials)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginCredentials), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7230/api/Auth/Login", stringContent))
                {


                    string token = await response.Content.ReadAsStringAsync();

                    AppJsonResponse responseJson = System.Text.Json.JsonSerializer.Deserialize<AppJsonResponse>(token);

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Incorrect UserId or Password!";
                    }
                    else
                    {
                        HttpContext.Session.SetString("JWToken", responseJson.token);
                        return Redirect("~/Books/Index");
                    }
                }
            }
            return View();
        }

    }
}
