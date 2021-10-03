using BookShop.Domain.DbContexts;
using BookShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookShop.Domain.SeedData
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly BookShopContext _ctxt;
        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager, BookShopContext ctx)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _ctxt = ctx;
        }

        public void InitSeedData()
        {
            InitUserData();
            InitAuthorsData();
            InitBookData();
        }

        public void InitUserData()
        {
            if (!_userManager.Users.Any())
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
                if (String.IsNullOrEmpty(assemblyPath))
                    return;

                var rootpath = assemblyPath + @"\SeedData\SeedData.json";
                var userData = File.ReadAllText(rootpath.Replace("BookShop.API", "BookShop.Domain"));
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role {Name = "Customer"},
                    new Role {Name = "Admin"},
                    new Role {Name = "Staff"},
                    new Role {Name = "VIP"}
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user, "Customer").Wait();
                }

                var adminUser = new User
                {
                    UserName = "Admin"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Staff" }).Wait();
                }
            }
        }

        public void InitAuthorsData()
        {
            if (!_ctxt.Authors.Any())
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
                if (String.IsNullOrEmpty(assemblyPath))
                    return;

                var rootpath = assemblyPath + @"\SeedData\AuthorsSeed.json";
                var authorsData = File.ReadAllText(rootpath.Replace("BookShop.API", "BookShop.Domain"));
                var authors = JsonConvert.DeserializeObject<List<Author>>(authorsData);

                foreach (var author in authors)
                {
                    author.CreatedBy = "Admin";
                    author.CreatedDate = DateTime.UtcNow;
                    author.UpdatedBy = "Admin";
                    author.UpdatedDate = DateTime.UtcNow;
                }

                _ctxt.AddRange(authors);
                _ctxt.SaveChanges();
            }
        }

        public void InitBookData()
        {
            if (!_ctxt.Books.Any())
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
                if (String.IsNullOrEmpty(assemblyPath))
                    return;

                var rootpath = assemblyPath + @"\SeedData\BookSeed.json";
                var booksData = File.ReadAllText(rootpath.Replace("BookShop.API", "BookShop.Domain"));
                var books = JsonConvert.DeserializeObject<List<Book>>(booksData);

                foreach (var book in books)
                {
                    book.CreatedBy = "Admin";
                    book.CreatedDate = DateTime.UtcNow;
                    book.UpdatedBy = "Admin";
                    book.UpdatedDate = DateTime.UtcNow;
                    book.publishedDate = DateTime.UtcNow;                  
                }

                _ctxt.AddRange(books);
                _ctxt.SaveChanges();
            }
        }
    }
}
