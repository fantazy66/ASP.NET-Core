namespace Shop.Web.ViewModels.ArtProducts
{
    using Shop.Data.Models;
    using Shop.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
