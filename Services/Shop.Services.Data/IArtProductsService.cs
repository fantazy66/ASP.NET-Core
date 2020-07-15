using Microsoft.AspNetCore.Http;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Data
{
    public interface IArtProductsService
    {
        Task<int> CreateAsync(
            string title,
            string size,
            decimal price,
            string description,
            DateTime artCreatedDate,
            List<ImageOfProduct>imageUrls,
            int categoryId,
            string userId,
            string artistName,
            string artistNationality,
            string artistBiography,
            DateTime artistBirthDate,
            DateTime artistDeathDate);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
