namespace Shop.Web.ViewModels.ArtProducts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Shop.Data.Models;
    using Shop.Services.Mapping;

    public class ArtProductCreateInputModel : IMapTo<ArtProduct>
    {
        public ArtProductCreateInputModel()
        {
            this.ImageLinks = new HashSet<IFormFile>();
        }

        [Required]
        public string Title { get; set; }

        public string Size { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ArtDescription { get; set; }

        public virtual ICollection<IFormFile> ImageLinks { get; set; }

        [Required]
        public DateTime ArtCreatedDate { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
