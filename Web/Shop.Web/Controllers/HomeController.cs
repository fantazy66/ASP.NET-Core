﻿namespace Shop.Web.Controllers
{
    using System.Diagnostics;
    using Shop.Data;
    using Shop.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Shop.Services;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly IMailService mailService;
        private readonly ApplicationDbContext context;

        // TODO vkarvame dbcontex-a v constructor-a, za da mojem da polzvame dannite ot bazata v actioni-te. 
        public HomeController(IMailService mailService, ApplicationDbContext context)
        {
            this.mailService = mailService;
            this.context = context;
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

        public IActionResult Shop()
        {
            var results = this.context.Products
                .OrderBy(p => p.Category)
                .ToList();

            // TODO taka shte izpolzvame dannite ot bazata vuv view-to.
            return this.View(results);
        }
    }
}
