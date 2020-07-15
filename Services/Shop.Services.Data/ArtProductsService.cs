using Shop.Data.Common.Repositories;
using Shop.Data.Models;
using Shop.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Data
{
    public class ArtProductsService : IArtProductsService
    {
        private readonly IDeletableEntityRepository<ArtProduct> artProductsRepository;

        public ArtProductsService(IDeletableEntityRepository<ArtProduct> artProductsRepository)
        {
            this.artProductsRepository = artProductsRepository;
        }

        public async Task<int> CreateAsync(
            string title, string size, decimal price,
            string description, DateTime artCreatedDate, List<ImageOfProduct>imageUrls,
            int categoryId, string userId,
            string artistName, string artistNationality, string artistBiography, DateTime artistBirthDate, DateTime artistDeathDate)
        {
            var artProduct = new ArtProduct
            {
                Title = title,
                Size = size,
                Price = price,
                ArtDescription = description,
                ArtCreatedDate = artCreatedDate,
                ImageLinks = imageUrls,
                UserId = userId,
                CategoryId = categoryId,
                Artist = new Artist
                {
                    Name = artistName,
                    Nationality = artistNationality,
                    Biography = artistBiography,
                    BirthDate = artistBirthDate,
                    DeathDate = artistDeathDate,
                },

            };

            await this.artProductsRepository.AddAsync(artProduct);

            await this.artProductsRepository.SaveChangesAsync();
            return artProduct.Id;
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.artProductsRepository
                            .All()
                            .OrderByDescending(x => x.CreatedOn)
                            .Where(x => x.CategoryId == categoryId)
                            .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var artProduct = this.artProductsRepository.All().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();
            return artProduct;
        }

        public int GetCountByCategoryId(int categoryId)
        {
            return this.artProductsRepository.All().Count(x => x.CategoryId == categoryId);
        }
    }
}
