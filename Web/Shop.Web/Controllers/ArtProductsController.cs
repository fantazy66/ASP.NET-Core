using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Web.ViewModels;
using Shop.Services.Data;
using Microsoft.AspNetCore.Identity;
using Shop.Web.ViewModels.ArtProducts;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Web.Controllers
{
    public class ArtProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IArtProductsService artProductsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArtProductsController(
            ApplicationDbContext context,
            IArtProductsService artProductsService,
            ICategoriesService categoriesService,
                        UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.artProductsService = artProductsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            var artProductViewModel = this.artProductsService.GetById<ArtProductViewModel>(id);
            if (artProductViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(artProductViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new ArtProductCreateInputModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ArtProductCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var artProductId = await this.artProductsService.CreateAsync(
                input.Title, input.Size, input.Price, input.ArtDescription, input.ArtCreatedDate, input.ImageLink,
                input.CategoryId, user.Id, input.Artist.Name, input.Artist.Nationality, input.Artist.Biography,
                input.Artist.BirthDate, input.Artist.DeathDate);

            this.TempData["InfoMessage"] = "ArtProduct has been listed!";
            return this.RedirectToAction(nameof(this.ById), new { id = artProductId });
        }
    }
}
