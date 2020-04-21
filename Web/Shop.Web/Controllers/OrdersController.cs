using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Data;
using Shop.Data.Common.Repositories;
using Shop.Data.Models;
using Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Nqma da izpolzvame cookies, a jwt. Sega ako ne sme lognati i se opitame da vlezem v shopstranicata, ni dava 401 greshka- unauthorized.
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<ProductsController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(ILogger<ProductsController> logger, ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {

            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var username = User.Identity.Name;
                var user = await this.userManager.GetUserAsync(this.User);

                // TODO .include za da mojem da vzemem sudurjanieto na vutreshnata kolekciq ot itemi. theninclude- za detaili na samiq item.
                // TODO automapvane na kolekciqta ot orderi, za da se vizualizira samo id, ordernumber i orderdate.
                return this.Ok(mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(
                                       this.context.Orders
                                           .Where(u => u.ApplicationUser.UserName == user.UserName)
                                           .Include(o => o.Items)
                                           .ThenInclude(i => i.Product)
                                           .ToList()));
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
            { //TODO automapper-a sluji za da pokazva v site-a order-a kakto e vuv view modela- samo 3 propertyta.
                if (order != null) return this.Ok(mapper.Map<Order, OrderViewModel>(order));
                else return this.NotFound();
            }
            catch (Exception ex)
            {

                this.logger.LogError($"Failed to get orders: {ex}");
                return this.BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        
        public async Task<IActionResult> Post([FromBody]OrderViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                // TODO mapvame naobratno ot viewModel kum classa.
                var newOrder = this.mapper.Map<OrderViewModel, Order>(model);

                // Dopulnitelen nash validation.
                if (newOrder.OrderDate == DateTime.MinValue)
                {
                    newOrder.OrderDate = DateTime.Now;
                }

                // Vajno!! Ne mojem da dobavim user.identity.Name direktno kato suzdavame nova order, koito ima property user. Trqbva da bruknem v db- koeto e userManager i ottam da go vzemem.
                // User.Identity.Name idva ot jwt security tokenite, koito samo potvurdjavat che tova sme nie, ne moje tqhnoto value da go slagame kato suzdavame nov order.
                var currentUser = await this.userManager.FindByNameAsync(User.Identity.Name);
                newOrder.ApplicationUser = currentUser;



                this.context.Add(newOrder);
                this.context.SaveChanges();
                // Pak obrushtame mapvaneto, za da moje da vizualizirame ViewModel-a.
                return this.Created($"/api/orders/{newOrder.Id}", this.mapper.Map<Order, OrderViewModel>(newOrder));
            }
            else
            {
                this.logger.LogError($"Failed to save a new order.");
                return this.BadRequest(this.ModelState);
            }
        }
    }

}
