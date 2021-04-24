using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class HomepageInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<HomeImageDTO> Images { get; set; }
        public List<string> ImageUrls { get; set; }

        [Required(ErrorMessage = "Please enter info")]
        public string Info { get; set; }
    }
}
