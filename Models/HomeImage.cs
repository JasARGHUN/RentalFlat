using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class HomeImage
    {
        public int Id { get; set; }
        public int HomepageInfoId { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("HomepageInfoId")]
        public virtual HomepageInfo Homepage { get; set; }
    }
}
