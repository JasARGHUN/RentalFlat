using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RentalFlat_Server.Helpers
{
    public static class IJSRuntimeExtension
    {
        // Don't forget to add this class to _Imports.razor
        public static async ValueTask ToastrSuccess(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }

        public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}
