namespace Models.DTOs
{
    public class AuthenticationResponseDTO
    {
        public bool IsAuthenticationSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
