using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class FlatImage
    {
        public int Id { get; set; }
        public int FlatId { get; set; }
        public string FlatImageUrl { get; set; }

        [ForeignKey("FlatId")]
        public virtual Flat Flat { get; set; }
    }
}
