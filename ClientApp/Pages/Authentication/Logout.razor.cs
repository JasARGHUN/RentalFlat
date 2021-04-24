using Microsoft.AspNetCore.Components;
using Models.DTOs;
using ClientApp.Services.IServices;
using System;
using System.Threading.Tasks;
using System.Web;

namespace ClientApp.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await authenticationService.Logout();
            navigationManager.NavigateTo("/");
        }
    }
}
