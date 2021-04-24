using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class FlatDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter flat name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter flat area")]
        public string FlatArea { get; set; }

        [Range(1, 300, ErrorMessage = "Price must be between 1 and 300")]
        public int RoomCount { get; set; }

        [Range(1, 3000000, ErrorMessage = "Price must be between 1 and 3 000 000")]
        public double Price { get; set; }

        public string Description { get; set; }

        public virtual ICollection<FlatImageDTO> FlatImages { get; set; }

        public List<string> ImageUrls { get; set; }
        public bool IsBooked { get; set; }

        public double TotalDays { get; set; }
        public double TotalAmount { get; set; }
    }
}
