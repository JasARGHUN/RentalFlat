using Microsoft.AspNetCore.Components;
using Models.DTOs;
using ClientApp.Services.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Pages.Authentication
{
    public partial class Register
    {
        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        private UserRequestDTO User = new UserRequestDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        private async Task Registration()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;

            var result = await authenticationService.RegisterUser(User);

            if (result.IsRegisterationSuccessful)
            {
                IsProcessing = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
        }
    }
}
