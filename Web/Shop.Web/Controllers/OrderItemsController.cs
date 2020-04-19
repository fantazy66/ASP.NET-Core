using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(ApplicationDbContext context,
            ILogger<OrderItemsController> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        // Vzimane na vsichki OrderItems.
        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = this.context
                            .Orders
                            .Include(o=> o.Items)
                            .ThenInclude(i => i.Product)
                            .Where(o => o.Id == orderId)
                            .FirstOrDefault();

            if (order != null)
            {
                return this.Ok(this.mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemsViewModel>>(order.Items));
            }
            else
            {
                return this.NotFound();
            }
        }

        // Vzimane na vseki otdelen OrderItem.
        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = this.context
                                      .Orders
                                      .Include(o => o.Items)
                                      .ThenInclude(i => i.Product)
                                      .Where(o => o.Id == orderId)
                                      .FirstOrDefault();

            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return this.Ok(this.mapper.Map<OrderItem, OrderItemsViewModel>(item));
                }
            }

            return this.NotFound();
        }
    }
}
