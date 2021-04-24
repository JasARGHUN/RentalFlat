using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.DTOs;
using Newtonsoft.Json;
using ClientApp.Services.IServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ClientApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO user)
        {
            var content = JsonConvert.SerializeObject(user);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/account/signin", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemAsync(StaticDetails.Local_Token, result.Token);
                await _localStorageService.SetItemAsync(StaticDetails.Local_UserDetails, result.UserDTO);

                ((AuthStateProvider)_authenticationStateProvider).NotifyUserLoggedIn(result.Token);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return new AuthenticationResponseDTO { IsAuthenticationSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(StaticDetails.Local_Token);
            await _localStorageService.RemoveItemAsync(StaticDetails.Local_UserDetails);

            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO user)
        {
            var content = JsonConvert.SerializeObject(user);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/account/signup", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegistrationResponseDTO { IsRegisterationSuccessful = true };
            }
            else
            {
                return result;
            }
        }
    }
}
