using Microsoft.AspNetCore.Components;
using Models.DTOs;
using ClientApp.Services.IServices;
using System;
using System.Threading.Tasks;
using System.Web;

namespace ClientApp.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationDTO User = new AuthenticationDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowAuthenticationErrors { get; set; }
        public string Error { get; set; }
        public string ReturnUrl { get; set; }
        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task LogInUser()
        {
            ShowAuthenticationErrors = false;
            IsProcessing = true;

            var result = await authenticationService.Login(User);

            if (result.IsAuthenticationSuccessful)
            {
                IsProcessing = false;

                var uri = new Uri(navigationManager.Uri);
                var query = HttpUtility.ParseQueryString(uri.Query);
                ReturnUrl = query["returnUrl"];

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }
            }
            else
            {
                IsProcessing = false;
                Error = result.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
        }
    }
}
