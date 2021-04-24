using System.Collections.Generic;

namespace Models.DTOs
{
    public class RegistrationResponseDTO
    {
        public bool IsRegisterationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
