namespace Shop.Web.ViewModels.ArtProducts
{
    using System;

    using Shop.Data.Models;
    using Shop.Services.Mapping;
    using AutoMapper;
    using Ganss.XSS;

    public class ArtProductViewModel : IMapFrom<ArtProduct>, IMapTo<ArtProduct>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        // Proverka ako ima error.
        public string ArtistName { get; set; }

        public string ArtistNationality { get; set; }

        public string Title { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string ArtDescription { get; set; }

        // Security purpose, avoiding xss.
        public string SanitizedArtDescription => new HtmlSanitizer().Sanitize(this.ArtDescription);

        public string ImageLink { get; set; }

        public DateTime ArtCreatedDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ArtProduct, ArtProductViewModel>()
                .ForMember(x => x.ArtistName, options =>
                {
                    options.MapFrom(x => x.Artist.Name);
                });
        }
    }
}
