using System.Collections.Generic;

namespace Models
{
    public class HomepageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual ICollection<HomeImage> Images { get; set; }
    }
}
