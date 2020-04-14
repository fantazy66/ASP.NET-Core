namespace Shop.Web.Controllers
{
    using System.Diagnostics;

    using Shop.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Shop.Services;

    public class HomeController : BaseController
    {
        private readonly IMailService mailService;

        public HomeController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        public IActionResult Index()
        {
            return this.View();
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
                this.mailService.SendMessage("apsd@abv.bg", model.Subject, $"From:{model.Name} {model.Email}, Message: {model.Message}");
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
    }
}
