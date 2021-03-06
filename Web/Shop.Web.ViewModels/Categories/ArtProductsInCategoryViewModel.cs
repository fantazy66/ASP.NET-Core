﻿namespace Shop.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;

    using Shop.Data.Models;

    using Shop.Services.Mapping;

    public class ArtProductsInCategoryViewModel : IMapFrom<ArtProduct>
    {
        public int Id { get; set; }

        public DateTime ArtCreatedDate { get; set; }

        public string Title { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string ArtDescription { get; set; }

        public virtual ICollection<ImageOfProduct> ImageLinks { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public string UserUserName { get; set; }
    }
}
