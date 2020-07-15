namespace Shop.Web
{
    using System.Reflection;

    using System;

    using Shop.Data;
    using Shop.Common;
    using Shop.Data.Common;
    using Shop.Data.Common.Repositories;
    using Shop.Data.Models;
    using Shop.Data.Repositories;
    using Shop.Data.Seeding;
    using Shop.Services.Data;
    using Shop.Services.Mapping;
    using Shop.Services.Messaging;
    using Shop.Web.ViewModels;


    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Shop.Services;
    using Newtonsoft.Json;
    using AutoMapper;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Shop.Web.Infrastructure.Authorization;
    using CloudinaryDotNet;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            Account cloudinaryCredentials = new Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddResponseCaching();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            // Tova se vkluchva zaradi GDPR
            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            // Vidovete authentication, koito shte izpolzvame.

            // JWT Authentication services
            //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtTokenValidation:Secret"]));
            //services.Configure<TokenProviderOptions>(opts =>
            //{
            //    opts.Audience = this.configuration["JwtTokenValidation:Audience"];
            //    opts.Issuer = this.configuration["JwtTokenValidation:Issuer"];
            //    opts.Path = "/api/account/login";
            //    opts.Expiration = TimeSpan.FromDays(15);
            //    opts.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //});

            //services
            //     .AddAuthentication()
            //     .AddJwtBearer(opts =>
            //     {
            //         opts.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = signingKey,
            //             ValidateIssuer = true,
            //             ValidIssuer = this.configuration["JwtTokenValidation:Issuer"],
            //             ValidateAudience = true,
            //             ValidAudience = this.configuration["JwtTokenValidation:Audience"],
            //             ValidateLifetime = true
            //         };
            //     });

            // Moje bi teq 2 service-a trqbva da se mahnat.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Suppress Multipart/form-data inference

                options.SuppressConsumesConstraintForFormFileParameters = true;

                // Suppress binding source attributes

                options.SuppressInferBindingSourcesForParameters = true;

                // Suppress automatic HTTP 400 errors

                options.SuppressModelStateInvalidFilter = true;

                // Suppress problem details responses

                options.SuppressMapClientErrors = true;
            });

            // TODO po default e vklucheno, no ne raboti post zaqvkite s nego.
            services.AddControllersWithViews(
            options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    });

            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Dava ni vuzmojnost da rabotim s public APIs, koito mai nqma da izpolzvam v proekta. Moje da se iztrie.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddNewtonsoftJson(opt =>
                                       opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            //services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IMailService, SendGridMailService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<IArtProductsService, ArtProductsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            services.AddTransient<IUserProfileService, UserProfileService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //       app.UseJwtBearerTokens(
            //app.ApplicationServices.GetRequiredService<IOptions<TokenProviderOptions>>(),
            //PrincipalResolver);

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            "artProductsInCategory",
                            "f/{name:minlength(3)}",
                            new { controller = "Categories", action = "ByName" });

                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }

        //private static async Task<GenericPrincipal> PrincipalResolver(HttpContext context)
        //{
        //    var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
        //    var email = context.Request.Form["email"];
        //    var user = await userManager.FindByEmailAsync(email);
        //    if (user == null || user.IsDeleted)
        //    {
        //        return null;
        //    }

        //    var password = context.Request.Form["password"];

        //    var isValidPassword = await userManager.CheckPasswordAsync(user, password);
        //    if (!isValidPassword)
        //    {
        //        return null;
        //    }

        //    var roles = await userManager.GetRolesAsync(user);

        //    var identity = new GenericIdentity(email, "Token");
        //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

        //    return new GenericPrincipal(identity, roles.ToArray());
        //}

    }
}
