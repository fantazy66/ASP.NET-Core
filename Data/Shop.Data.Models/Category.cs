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

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ArtProduct> ArtProducts { get; set; }

        // Vidovete categorii
        //Painting = 1,
        //Photography = 2,
        //Sculpture = 3,
        //Drawings = 4,
        //Prints = 5,
        //Illustration = 6,
        //Jewelry = 7,
    }
}