namespace Shop.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Shop.Data.Common.Repositories;
    using Shop.Data.Models;
    using Shop.Services.Mapping;

    public class ArtistService : IArtistService
    {
        private readonly IDeletableEntityRepository<Artist> artistRepository;

        public ArtistService(IDeletableEntityRepository<Artist> artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public async Task<int> CreateAsync(string name, string nationality, string biography, DateTime birthdate, DateTime deathdate)
        {
            var artist = new Artist
            {
                Name = name,
                Nationality = nationality,
                Biography = biography,
                BirthDate = birthdate,
                DeathDate = deathdate,
            };

            await this.artistRepository.AddAsync(artist);

            await this.artistRepository.SaveChangesAsync();
            return artist.Id;
        }

        public T GetById<T>(int id)
        {
            var artist = this.artistRepository.All().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();
            return artist;
        }
    }
}
