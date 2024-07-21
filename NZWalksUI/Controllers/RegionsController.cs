using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                // Get all regions from api
                var client = _httpClientFactory.CreateClient();
                var httpResponseMsg = await client.GetAsync("https://localhost:7231/api/regions");
                httpResponseMsg.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMsg.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception)
            {

                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addModel)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMsg = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7231/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addModel), Encoding.UTF8, "application/json")
            };

            var httpResponseMsg = await client.SendAsync(httpRequestMsg);
            httpResponseMsg.EnsureSuccessStatusCode();

            var response = await httpResponseMsg.Content.ReadFromJsonAsync<RegionDto>();
            if (response != null)
            {
                return RedirectToAction("index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMsg = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7231/api/regions/{id.ToString()}");
            if (responseMsg != null)
            {
                return View(responseMsg);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMsg = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7231/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var responseMsg = await client.SendAsync(httpRequestMsg);
            responseMsg.EnsureSuccessStatusCode();

            var response = await responseMsg.Content.ReadFromJsonAsync<RegionDto>();
            if (response != null)
            {
                return RedirectToAction("index", "Regions");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var responseMsg = await client.DeleteAsync($"https://localhost:7231/api/regions/{request.Id}");
                responseMsg.EnsureSuccessStatusCode();
                return RedirectToAction("index", "Regions");
            }
            catch (Exception)
            {
            }

            return View("Edit");
        }
    }
}
