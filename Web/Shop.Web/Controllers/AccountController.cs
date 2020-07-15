using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shop.Data.Models;
using Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Shop.Data;
using Microsoft.Extensions.Options;

namespace Shop.Web.Controllers
{
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.Extensions.Configuration.IConfiguration config;
        private readonly ApplicationDbContext context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginInputModel> _logger;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginInputModel> logger,
            UserManager<ApplicationUser> userManager, IConfiguration config, ApplicationDbContext context)
        {
            _userManager = userManager;
            this.config = config;
            this.context = context;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> WhoAmI()
        {
            return "Username:  " + this.User.Identity.Name;
        }

        [HttpGet]
        public ActionResult<string> Login(string username, string passwrod)
        {
            if (username == "petko12@abv.bg" && passwrod == "123456")
            {
                var secret = "KgpMkpTeFna0W7T72672GvI4D4RPZy6XgBCR9U2d0haf1yyc8ZBgHVJd5FqWH69D";
                var key = Encoding.UTF8.GetBytes(secret);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddDays(7),
                    Subject = new ClaimsIdentity(new[]
                    {
                                          new Claim(ClaimTypes.Name, "Petko12@abv.bg"),
                                           new Claim(ClaimTypes.NameIdentifier, "123123213a")
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return jwt;
            }

            return this.Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {

                        var claims = new[]
                            {
                                                         new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                                                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                                    };
                        var keyvalue = " kgpmkptefna0w7t72672gvi4d4rpzy6xgbcr9u2d0haf1yyc8zbghvjd5fqwh69d2131adsaxasdadqwdqdqwdq";
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyvalue));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            this.config["Tokens:Issuer"],
                            this.config["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: credentials
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        };
                        return this.Created(" ", results);
                    }
                }
            }

            return this.BadRequest();
        }
    }
}

