namespace Shop.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Shop.Services.Data;
    using Shop.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 10;

        private readonly IArtProductsService artProductsService;
        private readonly ICategoriesService categoriesService;
        private readonly IHttpContextAccessor http;

        public CategoriesController(
            IArtProductsService artProductsService,
            ICategoriesService categoriesService,
            IHttpContextAccessor http)
        {
            this.artProductsService = artProductsService;
            this.categoriesService = categoriesService;
            this.http = http;
        }

        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel =
                this.categoriesService.GetByName<CategoryViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.ArtProducts = this.artProductsService.GetByCategoryId<ArtProductsInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.artProductsService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
