﻿namespace Shop.Web
{
    using System.Reflection;

    using Shop.Data;
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
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
    using Shop.Services;
    using Newtonsoft.Json;
    using AutoMapper;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

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
            //services.AddControllersWithViews(
            //options =>
            //    {
            //        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //    });

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
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IMailService, NullMailService>();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
