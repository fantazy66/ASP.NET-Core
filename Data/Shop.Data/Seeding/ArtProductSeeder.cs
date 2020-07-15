using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Seeding
{
    internal class ArtProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            // TODO podsigurqva dali ni e suzdadena bazata, predi da seedne dannite.
            dbContext.Database.EnsureCreated();

            // Proverqva dali e true ili false - dali ima nalichni products.
            if (dbContext.ArtProducts.Any())
            {
                return;
            }

            await dbContext.ArtProducts.AddAsync(new ArtProduct
            {
                Size = "Big",
                Price = 10,
                Title = "PopMusic",
                ArtDescription = "loveisart",
            });
        }
    }
}
