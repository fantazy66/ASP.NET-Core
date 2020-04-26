using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Data
{
    public interface IArtProductsService
    {
        Task<int> CreateAsync(string title,
            string size,
            decimal price,
            string description,
            DateTime artCreatedDate,
            string imageUrl,
            int categoryId,
            string userId,
            int artistId);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
