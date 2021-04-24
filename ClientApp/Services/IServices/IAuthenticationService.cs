using Models.DTOs;
using System.Threading.Tasks;

namespace ClientApp.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO user); // Signature must match with action-method SignUp in Account Controller(API).
        Task<AuthenticationResponseDTO> Login(AuthenticationDTO user); // Signature must match with action-method SignIn in Account Controller(API).
        Task Logout();  
    }
}
