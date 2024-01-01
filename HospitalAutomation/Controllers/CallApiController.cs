using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Numerics;
using System.Text;

namespace HospitalAutomation.Controllers
{
    public class CallApiController : Controller
    {
        public readonly IWebHostEnvironment _webHostEnvironment; //Image eklemek için bunu eklememiz lazım

        public CallApiController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:7176/api/Api");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Gallery>>(jsonString);
            return View(values);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddData()
        {
            return View();
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> AddData(Gallery gallery, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(wwwRootPath, @"img");
            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(imagePath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                gallery.PhotoImage = @"\img\" + file.FileName;
            }


            var httpClient = new HttpClient();
            var jsonGallery = JsonConvert.SerializeObject(gallery);
            StringContent content = new StringContent(jsonGallery, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:7176/api/Api", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(gallery);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> EditData(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:7176/api/Api/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonGallery = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Gallery>(jsonGallery);
                return View(values);
            }
            return RedirectToAction("Index");

        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> EditData(Gallery gallery, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(wwwRootPath, @"img");
            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(imagePath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                gallery.PhotoImage = @"\img\" + file.FileName;
            }


            var httpClient = new HttpClient();

            var jsonGallery = JsonConvert.SerializeObject(gallery);
            var content = new StringContent(jsonGallery, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7176/api/Api", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(gallery);

        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public async Task<IActionResult> DeleteData(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync("https://localhost:7176/api/Api/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
