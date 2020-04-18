using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Data;
using Shop.Data.Common.Repositories;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IDeletableEntityRepository<Product> repository;
        private readonly ILogger<ProductsController> logger;
        private readonly ApplicationDbContext context;

        public OrdersController(IDeletableEntityRepository<Product> repository, ILogger<ProductsController> logger, ApplicationDbContext context)
        {
            this.repository = repository;
            this.logger = logger;
            this.context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // TODO .include za da mojem da vzemem sudurjanieto na vutreshnata kolekciq ot itemi. theninclude- za detaili na samiq item.
                return this.Ok(this.context.Orders
                                           .Include(o => o.Items)
                                           .ThenInclude(i => i.Product)
                                           .ToList());
            }
            catch (Exception ex)
            {

                this.logger.LogError($"Failed to get orders: {ex}");
                return this.BadRequest("Failed to get orders");
            }
        }

        // TODO id:int pokazva kakuv tip danni ochakvame.
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = this.context
                                   .Orders
                                   .Where(o => o.Id == id)
                                   .Include(o => o.Items)
                                   .ThenInclude(i => i.Product)
                                   .FirstOrDefault();
            try
            {
                if (order != null) return this.Ok(order);
                else return this.NotFound();
            }
            catch (Exception ex)
            {

                this.logger.LogError($"Failed to get orders: {ex}");
                return this.BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public ActionResult<Order> Post(Order order)
        {            
            try
            {
                this.context.Add(order);
                this.context.SaveChanges();
                return this.Created($"/api/orders/{order.Id}", order);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to save a new order: {ex}");
            }
            return this.BadRequest("Failed to save new order.");


        }
    }

}
