//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Shop.Data;
//using Shop.Data.Common.Repositories;
//using Shop.Data.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Shop.Web.Controllers
//{
//    [Route("api/[Controller]")]
//    [ApiController]
//    [Produces("application/json")] //Tozi apicontroller vinagi shte vrushta json formati zaradi tozi attribute.

//    public class ProductsController : ControllerBase
//    {
//        // TODO ne se znae dali shte raboti repositoyryto, koeto e vmesto dbcontext-a.
//        private readonly IDeletableEntityRepository<ArtProduct> repository;
//        private readonly ILogger<ProductsController> logger;
//        private readonly ApplicationDbContext context;

//        public ProductsController(IDeletableEntityRepository<ArtProduct> repository, ILogger<ProductsController> logger, ApplicationDbContext context)
//        {
//            this.repository = repository;
//            this.logger = logger;
//            this.context = context;
//        }
        
//        // TODO Kogato pravim IActionResult dannite se vrushtat v razlichni formati- xml, json i t.n. spored tova kak potrebitelqt moje da gi obraboti.
//        // ActionResult ot Ienumerable pokazva kakuv tip danni shte vrushtame.
//        [HttpGet]
//        [ProducesResponseType(200)]  //TODO tezi attributi slujat ako se suzdavat public APIs.
//        [ProducesResponseType(400)]  //TODO davat vidovete response, koito shte se izpolzvat i te avtomatichno se napazvat kum koda bez da pishem this.OK.
//        public ActionResult<IEnumerable<ArtProduct>> Get()
//        {
//            try
//            {
//                return this.Ok(this.context.Products.ToList());
//            }
//            catch (Exception ex)
//            {
//                this.logger.LogError($"Failed to get products: {ex}");
//                return this.BadRequest("Failed to get products");
//            }



//        }
//    }
//}
