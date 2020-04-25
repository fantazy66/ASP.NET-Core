namespace Shop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Shop.Data.Common.Models;

    public class ArtProduct : BaseDeletableModel<int>
    {
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Artist Artist { get; set; }

        public int ArtistId { get; set; }

        public string Title { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string ArtDescription { get; set; }

        public string ImageLink { get; set; }

        public DateTime ArtCreatedDate { get; set; }
    }
}
