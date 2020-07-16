namespace Shop.Web.ViewModels
{
    using System.Collections.Generic;

    using Shop.Data.Models;

    public class ArtProductSearchViewModel
    {
        public List<ArtProduct> ArtProducts { get; set; }

        public string SearchString { get; set; }
    }
}
