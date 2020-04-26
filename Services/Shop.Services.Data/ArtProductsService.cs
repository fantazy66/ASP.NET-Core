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

        public async Task<int> CreateAsync(string title, string size, decimal price, string description, DateTime artCreatedDate, string imageUrl, int categoryId, string userId, int artistId)
        {
            var artProduct = new ArtProduct
            {
                Title = title,
                Size = size,
                Price = price,
                ArtDescription = description,
                ArtCreatedDate = artCreatedDate,
                ImageLink = imageUrl,
                UserId = userId,
                CategoryId = categoryId,
                ArtistId = artistId,

            };

            await this.artProductsRepository.AddAsync(artProduct);

            await this.artProductsRepository.SaveChangesAsync();
            return artProduct.Id;
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.artProductsRepository
                            .All()
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
