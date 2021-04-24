using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class InfoBlock
    {
        public int Id { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
