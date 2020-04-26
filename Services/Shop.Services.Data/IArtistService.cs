namespace Shop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArtistService
    {
        Task<int> CreateAsync(string name, string nationality, string biography, DateTime birthdate, DateTime deathdate);

        T GetById<T>(int id);
    }
}
