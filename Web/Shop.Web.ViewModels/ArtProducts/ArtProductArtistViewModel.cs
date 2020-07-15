using Shop.Data.Models;
using Shop.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Web.ViewModels.ArtProducts
{
  public class ArtProductArtistViewModel : IMapFrom<Artist>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public string Biography { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeathDate { get; set; }
    }
}
