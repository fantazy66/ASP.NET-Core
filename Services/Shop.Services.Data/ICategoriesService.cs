using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Data
{
   public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
