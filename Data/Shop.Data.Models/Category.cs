namespace Shop.Data.Models
{
    using System.Collections.Generic;

    using Shop.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.ArtProducts = new HashSet<ArtProduct>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ArtProduct> ArtProducts { get; set; }
    }
}