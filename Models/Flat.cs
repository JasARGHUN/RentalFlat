using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Flat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FlatArea { get; set; }

        [Required]
        public int RoomCount { get; set; }

        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<FlatImage> FlatImages { get; set; }
    }
}
