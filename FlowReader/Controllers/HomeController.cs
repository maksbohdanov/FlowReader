using FlowReader.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlowReader.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorDetailsJson = TempData["ErrorDetails"] as string;
            if (string.IsNullOrEmpty(errorDetailsJson))
            {
                return View();
            }

            var errorDetails = JsonConvert.DeserializeObject<ApiResult>(errorDetailsJson);

            return View(errorDetails);
        }
    }
}
