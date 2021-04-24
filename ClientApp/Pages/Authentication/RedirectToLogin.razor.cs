using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace ClientApp.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }

        private bool notAuthorized { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationState;

            if(authState?.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    NavigationManager.NavigateTo("login", true);
                }
                else
                {
                    NavigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                notAuthorized = true;
            }
        }
    }
}
