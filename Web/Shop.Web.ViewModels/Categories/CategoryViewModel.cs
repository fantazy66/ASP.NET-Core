namespace Shop.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using Shop.Data.Models;
    using Shop.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ArtProductsInCategoryViewModel> ArtProducts { get; set; }
    }
}
