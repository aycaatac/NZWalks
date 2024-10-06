using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {

            List<RegionDto> response = new List<RegionDto>();
            try
            {
                //get all regions from web api
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7284/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode(); //throws exception if its not a successful message

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
                
            }
            catch(Exception ex)
            {
                
            }          

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel regionViewModel)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpReqMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7284/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(regionViewModel), Encoding.UTF8, "application/json")
                };

                var httpResponseMessage = await client.SendAsync(httpReqMessage);

                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
                if(response is not null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return View();
        }
    }
}
