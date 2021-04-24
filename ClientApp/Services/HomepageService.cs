using ClientApp.Services.IServices;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Models.DTOs;

namespace ClientApp.Services
{
    public class HomepageService : IHomepageService
    {
        // HttpClient отвечает за отправку HTTP-запросов и получения HTTP-ответов от ресурса, идентифицированного URI(унифицированный идентификатор ресурса).
        private readonly HttpClient _httpClient;

        public HomepageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Calling the RentalFlat.API/Controllers/HomepageController. Don't forget to change settings in wwwroot/appsettings.json for your API URL!!!
        public async Task<IEnumerable<HomepageInfoDTO>> GetAllHomepageInfo()
        {
            var response = await _httpClient.GetAsync($"api/homepage");
            var content = await response.Content.ReadAsStringAsync();

            var objects = JsonConvert.DeserializeObject<IEnumerable<HomepageInfoDTO>>(content);

            return objects;
        }
    }
}
