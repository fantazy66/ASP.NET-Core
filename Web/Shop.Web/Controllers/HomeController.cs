namespace Shop.Web.Controllers
{
    using System.Diagnostics;
    using Shop.Data;
    using Shop.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Shop.Services;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Shop.Web.ViewModels.Home;
    using Shop.Services.Data;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    
    using CloudinaryDotNet;
        using System.Collections.Generic;

    public class HomeController : BaseController
    {
        private readonly IMailService mailService;
        private readonly ApplicationDbContext context;
        private readonly ICategoriesService categoriesService;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;



        // TODO vkarvame dbcontex-a v constructor-a, za da mojem da polzvame dannite ot bazata v actioni-te. 
        public HomeController(IMailService mailService, ApplicationDbContext context, ICategoriesService categoriesService, Cloudinary cloudinary, ICloudinaryService cloudinaryService)
        {
            this.mailService = mailService;
            this.context = context;
            this.categoriesService = categoriesService;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {

            await this.cloudinaryService.UploadAsync(this.cloudinary, files);

            return this.Redirect("/");
        }

        public IActionResult Index()
        {
            this.HttpContext.Session.SetString("Name", "Marin");

            var viewModel = new IndexViewModel
            {
                Categories =
                     this.categoriesService.GetAll<IndexCategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            // Pravi proverka dali propertytata ot ContactViewModel sa validni
            // Tova e validaciqta na server-side, dokato attributite sa na clienskata chast.
            if (this.ModelState.IsValid)
            {
                // send the email
                this.mailService.SendEmailAsync(model.Email, model.Subject, model.Message);
                this.ViewBag.UserMessage = "Mail Sent";

                // za da izchistim polencatata, kudeto sme vuvejdali.
                this.ModelState.Clear();
            }

            return this.View();
        }

        public IActionResult About()
        {
            this.ViewBag.Title = "About us";

            return this.View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var results = this.context.ArtProducts
                .OrderBy(p => p.Category)
                .ToList();

            // TODO taka shte izpolzvame dannite ot bazata vuv view-to.
            return this.View(results);
        }
    }
}
