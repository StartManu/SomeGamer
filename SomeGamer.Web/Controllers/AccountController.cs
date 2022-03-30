using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SomeGamer.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SomeGamer.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CriarUsuario() => View();

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(Person person)
        {
            using (var httpClient = new HttpClient())
            {
                Person personReceived = new Person();
                StringContent content = new StringContent(JsonConvert.SerializeObject(person),
                    Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44335/api/Account/CriarUser", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    personReceived = JsonConvert.DeserializeObject<Person>(apiResponse);
                }
            }
            return View("Index");
        }
    }
}
