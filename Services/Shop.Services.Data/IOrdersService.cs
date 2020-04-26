using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Shop.Data.Models;

namespace Shop.Services.Data
{
      public interface IOrdersService
    {
        Task<int> CreateOrder(string userId,string deliveryAddress);

        T GetById<T>(int id);

        IEnumerable<T> GetByUserId<T>(string userId, int? take = null, int skip = 0);

        Task<bool> CompleteOrder(int orderId);

        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
