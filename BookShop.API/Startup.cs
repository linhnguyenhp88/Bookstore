using System;
using System.Linq;
using System.Net;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookShop.Domain.DbContexts;
using BookShop.Domain.Entities;
using BookShop.Domain.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BookShop.Domain.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using BookShop.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using BookShop.Domain.Interfaces.IServices;
using BookShop.Domain.Interfaces.IRepository;
using Microsoft.Extensions.DependencyInjection;
using BookShop.Infrastructure.ExtensionMethods;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AWS.Logger;

namespace BookShop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            //Getting configs for AWS CloudWatch
            services.Configure<AWSLoggerConfig>(Configuration.GetSection("AWSLoggerConfig"));

            // prepare register dependency injection
            services.AddDbContext<BookShopContext>( // entity framework core DbContext, the default lifetime is 'scoped'
              contextOptionsBuilder =>
              {                  
                  contextOptionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                  //As we have many databases in the system
                  //contextOptionsBuilder.ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning));
              });

            //services.AddBookShopServices("BookShop.Domain","BookShop.Infrastructure");
            services.AddLogging(); // logging: this is equipvalent to .AddSingleton<ILoggingFactory, LoggingFactory>()
            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddAutoMapper();

            // Implement oath2
            IdentityBuilder build = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            build = new IdentityBuilder(build.UserType, typeof(Role), build.Services);
            build.AddEntityFrameworkStores<BookShopContext>();
            build.AddRoleValidator<RoleValidator<Role>>();
            build.AddRoleManager<RoleManager<Role>>();
            build.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                         .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("CustomerRole", policy => policy.RequireRole("Admin", "Staff","VIP","Customer"));
                options.AddPolicy("VipRole", policy => policy.RequireRole("VIP", "Customer"));
                options.AddPolicy("StaffRole", policy => policy.RequireRole("Admin", "Staff"));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add application services
            services.AddTransient<Seed>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(builder => {
                builder.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            seeder.InitSeedData();
            app.UseCors(x => 
            x.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
            app.UseAuthentication();
            app.UseMvc();

            loggerFactory.AddDebug(LogLevel.Debug);
        }
    }
}
