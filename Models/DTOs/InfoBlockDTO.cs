using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class InfoBlockDTO
    {
        public int Id { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
