namespace Shop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Shop.Data.Common.Repositories;
    using Shop.Data.Models;
    using Shop.Services.Mapping;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<int> CreateOrder(string userId, string deliveryAddress)
        {
            var order = new Order
            {
                UserId = userId,
                DeliveryAddress = deliveryAddress,
                Status = OrderStatus.Active,
                CreatedOn = DateTime.UtcNow,
            };
            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }

        public T GetById<T>(int id)
        {
            var order = this.ordersRepository.All().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();
            return order;
        }

        public IEnumerable<T> GetByUserId<T>(string userId, int? take = null, int skip = 0)
        {
            var query = this.ordersRepository
                           .All()
                           .Where(x => x.UserId == userId)
                           .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<bool> CompleteOrder(int orderId)
        {
            var order = this.ordersRepository.All()
               .FirstOrDefault(x => x.Id == orderId);

            if (order == null || order.Status != OrderStatus.Active)
            {
                throw new ArgumentException(nameof(order));
            }

            order.Status = OrderStatus.Completed;

            this.ordersRepository.Update(order);
            int result = await this.ordersRepository.SaveChangesAsync();

            return result > 0;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Order> query =
                this.ordersRepository.All().OrderBy(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
