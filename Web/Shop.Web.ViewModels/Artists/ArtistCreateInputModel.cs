namespace Shop.Web.ViewModels.Artists
{
    using System;

    using Shop.Data.Models;
    using Shop.Services.Mapping;

    public class ArtistCreateInputModel : IMapTo<Artist>
    {
        public string Name { get; set; }

        public string Nationality { get; set; }

        public string Biography { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeathDate { get; set; }
    }
}
